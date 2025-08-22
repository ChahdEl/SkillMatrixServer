public class UpdateLevelRequest
{
    public int Matricule { get; set; }  
    public int Level { get; set; }         
    public int Score { get; set; }      
    public string? CurrentStation { get; set; }    
    public bool[]? Answers { get; set; }     
}
