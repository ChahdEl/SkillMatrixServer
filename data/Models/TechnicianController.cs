using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Skill_Matrix_Serv.Data.Models
{
    [ApiController]
    public class TechnicianController : ControllerBase
    {
        private readonly IConfiguration _config;

        public TechnicianController(IConfiguration config)
        {
            _config = config;
        }

         [HttpGet("Get_verify")]
        public JsonResult VerifyTechnicianCode([FromQuery] string code, string pwd)
        {
            string query = @"
        SELECT COUNT(*) 
        FROM [Test_DB].[dbo].[Technician]
        WHERE [NetID] = @Code 
          AND [IsActive] = 1
          AND [mot_passe]=@pwd;
    ";

            string? sqlDataSource = _config.GetConnectionString("Test_DB");
            int matchCount = 0;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                try
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@Code", code);
                        myCommand.Parameters.AddWithValue("@pwd", pwd);
                        var result = myCommand.ExecuteScalar();
                        matchCount = result != null ? (int)result : 0;

                        System.Diagnostics.Debug.WriteLine($"Match count: {matchCount}");
                    }
                }
                catch (Exception er)
                {
                    System.Diagnostics.Debug.WriteLine($"Database error: {er.Message}");
                    return new JsonResult(new { success = false, error = er.Message });
                }
            }

            bool isValid = matchCount > 0;
            return new JsonResult(new { success = true, isValid });
        }
       
    }
}
