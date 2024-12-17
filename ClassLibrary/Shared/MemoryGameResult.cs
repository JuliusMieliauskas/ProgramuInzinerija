namespace Shared;

public class MemoryGameResult
{
    public int Id { get; set; }
    public String Difficulty { get; set; }
    public int Missmatches { get; set; }
    public DateTime Date { get; set; }

    public MemoryGameResult() {
        Date = DateTime.Now;
    }

    public MemoryGameResult(int missmatches, String difficulty, int id = 0)
    {
        Id = id;
        Difficulty = difficulty;
        Missmatches = missmatches;
        Date = DateTime.Now;
    }

    public MemoryGameResult(int missmatches)
    {
        Missmatches = missmatches;
    }
}