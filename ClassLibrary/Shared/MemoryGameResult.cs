namespace Shared;

public class MemoryGameResult
{
    public int Id { get; set; }
    public int Missmatches { get; set; }
    public DateTime Date { get; set; }

    public MemoryGameResult() {
        Date = DateTime.Now;
    }

    public MemoryGameResult(int missmatches, int id = 0)
    {
        Id = id;
        Missmatches = missmatches;
        Date = DateTime.Now;
    }
}