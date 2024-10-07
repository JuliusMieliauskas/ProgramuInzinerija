using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
namespace MyApp.Pages;

public class ReactionBase : ComponentBase
{
    Random rand = new Random();
    protected bool inAction = false;
    protected bool springUp = false;
    protected double timePassed = 0;
    protected string commencingText = "Wait for the button.";
    protected double reactionTime;
    protected double reactionSpringUp = 3;
    protected System.Timers.Timer reactionTimer = new System.Timers.Timer(50);
    public class ReactionResult{
        public double Time {get; set;}
        public DateTime date;
        public ReactionResult(double timeGiven){
            Time = timeGiven;
            date = DateTime.Now;
        }
    }
    protected List<ReactionResult> reactionResultList = new List<ReactionResult>();

    protected void TestKidou(){
        reactionSpringUp = rand.Next(2, 4);
        reactionSpringUp += rand.NextDouble();
        inAction = true;
        commencingText = "Wait for the button.";
        reactionTimer.Start();
        reactionTimer.Elapsed += (sender, e) => {
            timePassed += 0.05;
            if (timePassed >= reactionSpringUp){
                springUp = true;
                commencingText = "Press!";
            } 
            InvokeAsync(StateHasChanged);
        }; 
    }
    protected void TestEarly(){
        commencingText = "Too early! Wait for the button.";
        timePassed = 0;
    }
    protected void TestFinsh(){
        reactionTimer.Dispose();
        reactionTimer = new System.Timers.Timer(50);
        reactionTime = timePassed - reactionSpringUp;
        timePassed = 0;
        springUp = false;
        inAction = false;
    }
    protected void SaveResults(){
        if (reactionTime != 0){
            reactionResultList.Add(new ReactionResult(reactionTime));
            reactionTime = 0;
        }
    }

}