namespace Shared
{
    public class CalcGameResult
    {
        public int Id { get; set; }
        public required string Difficulty { get; set; } = "Easy";
        public int CorrectAnswers { get; set; }
        public int TotalRounds { get; set; }
    }
}

