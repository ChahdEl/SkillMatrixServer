using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Skill_Matrix_Serv.Data.Models
{
    [ApiController]
    public class LoginRequest
{
    public string? NetID { get; set; }
    public string? mdp { get; set; }
}
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        public AuthController(IConfiguration config)
        {
            _config = config;
        }

       [HttpPost("LOGIN")]
    public JsonResult LOGIN([FromBody] LoginRequest loginRequest)
    {
        string NetID = loginRequest.NetID;
        string mdp = loginRequest.mdp;

        string query = @"SELECT * FROM [Test_DB].[dbo].[TeamLeaders] 
                        WHERE NetID = @NetID AND mdp = @mdp AND IsActive = 1;";

        DataTable table = new DataTable();
        string? SqlDataSource = _config.GetConnectionString("Test_DB");
        SqlDataReader myReader;

        using (SqlConnection myCon = new SqlConnection(SqlDataSource))
        {
            try
            {
                myCon.Open();
            }
            catch (Exception er)
            {
                System.Diagnostics.Debug.WriteLine(er.Message);
            }

            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@NetID", NetID);
                myCommand.Parameters.AddWithValue("@mdp", mdp);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
            }
        }

        if (table.Rows.Count > 0)
        {
            return new JsonResult(new
            {
                success = true,
                message = "Authentication successful!",
                user = table
            });
        }
        else
        {
            return new JsonResult(new
            {
                success = false,
                message = "Invalid credentials or inactive account."
            });
        }
    }
/*******************************************************************************************************************************************************************/
        /*******************************************************************************************************************************************************************/
    }
}
