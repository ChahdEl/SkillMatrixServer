using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Skill_Matrix_Serv.Data.Models
{
    [ApiController]
    public class OperatorController : ControllerBase
    {
        private IConfiguration _config;
        public OperatorController(IConfiguration config)
        {
            _config=config;
        }

        [HttpGet("GET_Operators")]

        public JsonResult GET_Operators()
        {
            string query="select * from [Test_DB].[dbo].[Operators];";
            DataTable table=new DataTable();
            string? SqlDataSource = _config.GetConnectionString("Test_DB");
            SqlDataReader myReader;
            using(SqlConnection myCon=new SqlConnection(SqlDataSource))
            {
                
            try
                {
                myCon.Open();
                }
            catch (Exception er) {
                System.Diagnostics.Debug.WriteLine(er.Message);
                }

                using(SqlCommand myCommand=new SqlCommand(query,myCon))
                {
                    myReader=myCommand.ExecuteReader();
                    table.Load(myReader);
                    System.Diagnostics.Debug.WriteLine(table);
                    
                }

            }

            return new JsonResult(table);

        }









        [HttpGet("GET_Operators_By_TLNZ")]

        public JsonResult GET_Operators_By_TLNZ(string? NetID)
        {
            string query="select * from [Test_DB].[dbo].[Operators] where TeamLeader=@NetID and IsActive=1;";
            DataTable table=new DataTable();
            string? SqlDataSource = _config.GetConnectionString("Test_DB");
            SqlDataReader myReader;
            using(SqlConnection myCon=new SqlConnection(SqlDataSource))
            {
                
            try
                {
                myCon.Open();
                }
            catch (Exception er) {
                System.Diagnostics.Debug.WriteLine(er.Message);
                }

                using(SqlCommand myCommand=new SqlCommand(query,myCon))
                {
                    myCommand.Parameters.AddWithValue("@NetID",NetID);
                    myReader=myCommand.ExecuteReader();
                    table.Load(myReader);
                    System.Diagnostics.Debug.WriteLine(table);
                    
                }

            }

            return new JsonResult(table);

        }

        [HttpGet("GET_Operator_By_ID")]

        public JsonResult GET_Operator_By_ID(int Matricule)
        {
            string query="select * from [Test_DB].[dbo].[Operators] where Matricule=@Matricule;";
            DataTable table=new DataTable();
            string? SqlDataSource = _config.GetConnectionString("Test_DB");
            SqlDataReader myReader;
            using(SqlConnection myCon=new SqlConnection(SqlDataSource))
            {
                
            try
                {
                myCon.Open();
                }
            catch (Exception er) {
                System.Diagnostics.Debug.WriteLine(er.Message);
                }

                using(SqlCommand myCommand=new SqlCommand(query,myCon))
                {
                    myCommand.Parameters.AddWithValue("@Matricule",Matricule);
                    myReader=myCommand.ExecuteReader();
                    table.Load(myReader);
                    System.Diagnostics.Debug.WriteLine(table);
                    
                }

            }

            return new JsonResult(table);

        }

        [HttpGet("GET_Levels_By_Operator")]

        public JsonResult GET_Levels_By_Operator(int Matricule)
        {
            DataTable table=new DataTable();
            string? SqlDataSource = _config.GetConnectionString("Test_DB");
            SqlDataReader myReader;
            using(SqlConnection myCon=new SqlConnection(SqlDataSource))
            {
                
            try
                {
                myCon.Open();
                }
            catch (Exception er) {
                System.Diagnostics.Debug.WriteLine(er.Message);
                }

                using(SqlCommand myCommand=new SqlCommand((@"
                SELECT 
                        t1.LVLID,
                        t1.Score,
                        t3.Title,
                        t3.Description,
                        t3.QuestionCount,
                        t1.ANS1,
                        t1.ANS2,
                        t1.ANS3,
                        t1.ANS4,
                        t1.ANS5,
                        t1.ANS6,
                        t1.ANS7,
                        t1.ANS8,
                        t1.ANS9,
                        t1.ANS10,
                        t1.ANS11,
                        t1.ANS12,
                        t1.ANS13,
                        t1.ANS14,
                        t1.ANS15,
                        t1.ANS16,
                        t1.ANS17,
                        t1.ANS18,
                        t1.ANS19,
                        t1.ANS20
                FROM [Test_DB].[dbo].Answers t1 
                JOIN [Test_DB].[dbo].[Operators] t2 ON t2.matricule = t1.OperatorMat AND t1.OperatorMat=@Matricule AND t1.Station=t2.CurrentStation
                JOIN [Test_DB].[dbo].[Levels] t3 ON t3.LvlID = t1.LVLID
                ORDER BY t1.LVLID



                "),myCon))
                {
                    myCommand.Parameters.AddWithValue("@Matricule",Matricule);
                    myReader=myCommand.ExecuteReader();
                    table.Load(myReader);
                    System.Diagnostics.Debug.WriteLine(table);
                    
                }

            }

            return new JsonResult(table);

        }






        [HttpPost("SET_Operator")]

        public JsonResult SET_Operator(string NetID,int Matricule,string Name)
        {
            string query="Insert Into [Test_DB].[dbo].[Operators]([NetID],[Matricule],[Name],[IsActive]) values(@netID,@matricule,@name,0);";
            DataTable table=new DataTable();
            string? SqlDataSource = _config.GetConnectionString("Test_DB");
            SqlDataReader myReader;
            using(SqlConnection myCon=new SqlConnection(SqlDataSource))
            {
                
            try
                {
                myCon.Open();
                
                }
            catch (Exception er) {
                System.Diagnostics.Debug.WriteLine(er.Message);
                }

                using(SqlCommand myCommand=new SqlCommand(query,myCon))
                {
                    myCommand.Parameters.AddWithValue("@netID",NetID);
                    myCommand.Parameters.AddWithValue("@matricule",Matricule);
                    myCommand.Parameters.AddWithValue("@name",Name);
                    myReader=myCommand.ExecuteReader();
                    table.Load(myReader);

                }

            }

            return new JsonResult("Record added successfully !");

        }










        [HttpPost("Add_New_Operator")]

        public JsonResult Add_New_Operator(int Matricule,string Name,string Project,string CurrentStation,string CurrentLevel,string teamLeader)
        {
            
            DataTable table=new DataTable();
            string? SqlDataSource = _config.GetConnectionString("Test_DB");
            SqlDataReader myReader;
            using(SqlConnection myCon=new SqlConnection(SqlDataSource))
            {
                
            try
                {
                myCon.Open();
                
                }
            catch (Exception er) {
                System.Diagnostics.Debug.WriteLine(er.Message);
                }

                using(SqlCommand myCommand=new SqlCommand((@"
                INSERT INTO [dbo].[Operators]
                            ([Matricule]
                            ,[Name]
                            ,[Project]
                            ,[CurrentStation]
                            ,[CurrentLevel]
                            ,[TeamLeader]
                            ,[IsActive])
                 VALUES
                    (@matricule,@Name,@Project,@currentStation,@CurrentLevel,@TeamLeader,1);
                "),myCon))
                {
                    myCommand.Parameters.AddWithValue("@matricule",Matricule);
                    myCommand.Parameters.AddWithValue("@Name",Name);
                    myCommand.Parameters.AddWithValue("@Project",Project);
                    myCommand.Parameters.AddWithValue("@currentStation",CurrentStation);
                    myCommand.Parameters.AddWithValue("@CurrentLevel",CurrentLevel);
                    myCommand.Parameters.AddWithValue("@TeamLeader",teamLeader);
                    myReader=myCommand.ExecuteReader();
                    table.Load(myReader);

                }

            }

            return new JsonResult("Record added successfully !");

        }


        [HttpPost("Add_New_Operator_Levels")]

        public JsonResult Add_New_Operator_Levels(int OperatorMat,string TeamLeader,string Station)
        {
            
            DataTable table=new DataTable();
            string? SqlDataSource = _config.GetConnectionString("Test_DB");
            SqlDataReader myReader;
            using(SqlConnection myCon=new SqlConnection(SqlDataSource))
            {
                
            try
                {
                myCon.Open();
                
                }
            catch (Exception er) {
                System.Diagnostics.Debug.WriteLine(er.Message);
                }

                using(SqlCommand myCommand=new SqlCommand((@"
                INSERT INTO [dbo].[Answers]
                        ([OperatorMat]
                        ,[TeamLeader]
                        ,[Station]
                        ,[LVLID]
                        ,[Score])
                VALUES
                        (@OpMat,@tl,@station,0,0),
                        (@OpMat,@tl,@station,1,0),
                        (@OpMat,@tl,@station,2,0),
                        (@OpMat,@tl,@station,3,0),
                        (@OpMat,@tl,@station,4,0)
                "),myCon))
                {
                    myCommand.Parameters.AddWithValue("@OpMat",OperatorMat);
                    myCommand.Parameters.AddWithValue("@tl",TeamLeader);
                    myCommand.Parameters.AddWithValue("@Station",Station);

                    myReader=myCommand.ExecuteReader();
                    table.Load(myReader);

                }

            }

            return new JsonResult("Records added successfully !");

        }



[HttpPut("Update_Operator_By_Matricule")]
public JsonResult Update_Operator_By_Matricule(int Matricule, string Name, string Project, string CurrentStation, string CurrentLevel, string TeamLeader)
{
    string query = @"
        UPDATE [dbo].[Operators]
        SET 
            Name = @Name,
            Project = @Project,
            CurrentStation = @CurrentStation,
            CurrentLevel = @CurrentLevel,
            TeamLeader = @TeamLeader
        WHERE 
            Matricule = @Matricule";

    DataTable table = new DataTable();
    string? SqlDataSource = _config.GetConnectionString("Test_DB");
    SqlDataReader myReader;

    using(SqlConnection myCon = new SqlConnection(SqlDataSource))
    {
        try
        {
            myCon.Open();
        }
        catch (Exception er) 
        {
            System.Diagnostics.Debug.WriteLine(er.Message);
            return new JsonResult("Failed to connect to database.");
        }

        using(SqlCommand myCommand = new SqlCommand(query, myCon))
        {
            myCommand.Parameters.AddWithValue("@Matricule", Matricule);
            myCommand.Parameters.AddWithValue("@Name", Name);
            myCommand.Parameters.AddWithValue("@Project", Project);
            myCommand.Parameters.AddWithValue("@CurrentStation", CurrentStation);
            myCommand.Parameters.AddWithValue("@CurrentLevel", CurrentLevel);
            myCommand.Parameters.AddWithValue("@TeamLeader", TeamLeader);

            try
            {
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Update Error: " + ex.Message);
                return new JsonResult("Failed to update operator.");
            }
        }
    }

    return new JsonResult("Record updated successfully!");
}





[HttpGet("Check_Levels_By_OperatorAndStation")]
public JsonResult Check_Levels_By_OperatorAndStation(int Matricule, string Station)
{
    string query = @"
        SELECT COUNT(*) 
        FROM [dbo].[Answers]
        WHERE OperatorMat = @Matricule AND Station = @Station";

    DataTable table = new DataTable();
    string? SqlDataSource = _config.GetConnectionString("Test_DB");
    int levelCount = 0;

    using (SqlConnection myCon = new SqlConnection(SqlDataSource))
    {
        try
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@Matricule", Matricule);
                myCommand.Parameters.AddWithValue("@Station", Station);
                levelCount = (int)myCommand.ExecuteScalar();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error checking levels: " + ex.Message);
            return new JsonResult("Error occurred while checking levels.");
        }
    }

    return new JsonResult(levelCount > 0);
}











[HttpPut("Update_Level_Score_And_Answers")]
public IActionResult UpdateLevelScoreAndAnswers([FromBody] UpdateLevelRequest request)
{
    if (request == null || request.Answers == null)
    {
        return BadRequest("Invalid request or missing answers array.");
    }

    string query = @"
        UPDATE [dbo].[Answers]
        SET 
            Score = @Score,
            ANS1 = @ANS1, ANS2 = @ANS2, ANS3 = @ANS3, ANS4 = @ANS4, ANS5 = @ANS5,
            ANS6 = @ANS6, ANS7 = @ANS7, ANS8 = @ANS8, ANS9 = @ANS9, ANS10 = @ANS10,
            ANS11 = @ANS11, ANS12 = @ANS12, ANS13 = @ANS13, ANS14 = @ANS14, ANS15 = @ANS15,
            ANS16 = @ANS16, ANS17 = @ANS17, ANS18 = @ANS18, ANS19 = @ANS19, ANS20 = @ANS20
        WHERE OperatorMat = @Matricule AND LVLID = @Level AND Station=@CurrentStation";

    string? sqlDataSource = _config.GetConnectionString("Test_DB");
    using (SqlConnection myCon = new SqlConnection(sqlDataSource))
    {
        SqlCommand myCommand = new SqlCommand(query, myCon);
        myCommand.Parameters.AddWithValue("@Matricule", request.Matricule);
        myCommand.Parameters.AddWithValue("@Level", request.Level);
        myCommand.Parameters.AddWithValue("@Score", request.Score);
        myCommand.Parameters.AddWithValue("@CurrentStation", request.CurrentStation);

        // Add each answer (assuming answers array length is 20)
        for (int i = 0; i < request.Answers.Length; i++)
        {
            myCommand.Parameters.AddWithValue($"@ANS{i + 1}", request.Answers[i]);
        }

        myCon.Open();
        int rowsAffected = myCommand.ExecuteNonQuery();
        return rowsAffected > 0 ? new JsonResult("Level answers updated successfully") : BadRequest("Failed to update answers");
    }
}








[HttpPut("Update_Operator_Level")]
public IActionResult UpdateOperatorLevel([FromBody] UpdateOperatorLevelRequest request)
{
    string query = @"
        UPDATE [dbo].[Operators]
        SET CurrentLevel = @NewLevel
        WHERE Matricule = @Matricule";

    string? sqlDataSource = _config.GetConnectionString("Test_DB");
    using (SqlConnection myCon = new SqlConnection(sqlDataSource))
    {
        SqlCommand myCommand = new SqlCommand(query, myCon);
        myCommand.Parameters.AddWithValue("@Matricule", request.matricule);
        myCommand.Parameters.AddWithValue("@NewLevel", request.newLevel);

        myCon.Open();
        int rowsAffected = myCommand.ExecuteNonQuery();
        return rowsAffected > 0 ? new JsonResult("Operator level updated successfully") : BadRequest("Failed to update operator level");
    }
}










[HttpPut("Update_Operator_IsActive")]
public IActionResult UpdateOperatorIsActive(int Matricule)
{
    string query = @"
        UPDATE [dbo].[Operators]
        SET IsActive = 0
        WHERE Matricule = @Matricule";

    DataTable table = new DataTable();
    string? SqlDataSource = _config.GetConnectionString("Test_DB");
    using (SqlConnection myCon = new SqlConnection(SqlDataSource))
    {
        try
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@Matricule", Matricule);
                int rowsAffected = myCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return new JsonResult("Operator deleted successfully!");
                }
                else
                {
                    return new JsonResult("Operator not found or already inactive.");
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error updating IsActive status: " + ex.Message);
            return new JsonResult("An error occurred while deactivating the operator.");
        }
    }
}






[HttpGet("GET_Disabled_Operators_By_TLNZ")]
public JsonResult GET_Disabled_Operators_By_TLNZ(string NetID)
{
    string query = "SELECT * FROM [Test_DB].[dbo].[Operators] WHERE TeamLeader = @NetID AND IsActive = 0;";
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
            myReader = myCommand.ExecuteReader();
            table.Load(myReader);
        }
    }

    return new JsonResult(table);
}







[HttpGet("GET_PastStations_By_Operator")]
public JsonResult GET_PastStations_By_Operator(int Matricule)
{
    string query = @"
        SELECT DISTINCT 
            Station, 
            LVLID, 
            Score
        FROM [Test_DB].[dbo].[Answers]
        WHERE OperatorMat = @Matricule
        and Score<>0
        ORDER BY Station, LVLID";

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
            myCommand.Parameters.AddWithValue("@Matricule", Matricule);
            myReader = myCommand.ExecuteReader();
            table.Load(myReader);
        }
    }

    return new JsonResult(table);
}





    }  
}
