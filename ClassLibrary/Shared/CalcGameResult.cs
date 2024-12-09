namespace Shared;
public class CalcGameResult
{
    public int Id { get; set; }
    public string? Difficulty { get; set; }
    public int Score { get; set; }
    public DateTime Date { get; set; }
    public CalcGameResult(){}
    public CalcGameResult(string difficulty, int score, DateTime date, int id = 0)
    {
        Difficulty = difficulty;
        Score = score;
        Id = id;
        Date = date;
    }
    public CalcGameResult(string difficulty, int score, int id = 0)
    {
        Difficulty = difficulty;
        Score = score;
        Id = id;
        Date = DateTime.Now;
    }
}

