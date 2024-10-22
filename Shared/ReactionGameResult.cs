namespace Shared;

public class ReactionGameResult
{
    public int Id { get; set; }
    public double ReactionTime { get; set; }
    public DateTime Date { get; set; }

    public ReactionGameResult() {
        Date = DateTime.Now;
    }

    public ReactionGameResult(int id, int reactionTime)
    {
        Id = id;
        ReactionTime = reactionTime;
        Date = DateTime.Now;
    }
}
