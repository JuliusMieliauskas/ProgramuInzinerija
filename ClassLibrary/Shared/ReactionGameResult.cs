namespace Shared;

public class ReactionGameResult
{
    public int Id { get; set; }
    public double ReactionTime { get; set; }
    public DateTime Date { get; set; }

    public ReactionGameResult() {
        Date = DateTime.Now;
    }

    public ReactionGameResult(double reactionTime, int id = 0)
    {
        Id = id;
        ReactionTime = reactionTime;
        Date = DateTime.Now;
    }
}
