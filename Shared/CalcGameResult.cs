namespace Shared
{
    public class CalcGameResult
    {
        public int Id { get; set; }
        public required string Difficulty { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
    }
}

