public class UpdateLevelRequest
{
    public int Matricule { get; set; }      // integer
    public int Level { get; set; }          // integer
    public int Score { get; set; }          // integer
    public string? CurrentStation { get; set; }    
    public bool[]? Answers { get; set; }     // boolean array (e.g., [true, false, true, ...])
}
