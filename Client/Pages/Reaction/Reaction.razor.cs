using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http.Json;
using System.Net.Http;
using Shared;

namespace Client.Pages;
public class ReactionBase : ComponentBase
{
    Random rand = new Random();
    public bool inAction = false;
    public bool springUp = false;
    public double timePassed = 0;
    public string commencingText = "Wait for the button.";
    public double reactionTime;
    public double reactionSpringUp = 3;
    public System.Timers.Timer reactionTimer = new System.Timers.Timer(50);
    
    [Inject]
    private HttpClient _httpClient { get; set; }
    
    public List<ReactionGameResult> reactionResultList = new List<ReactionGameResult>();

    public void TestStart(){
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
    public void TestEarly(){
        commencingText = "Too early! Wait for the button.";
        timePassed = 0;
    }
    public void TestFinish(){
        reactionTimer.Dispose();
        reactionTimer = new System.Timers.Timer(50);
        reactionTime = timePassed - reactionSpringUp;
        timePassed = 0;
        springUp = false;
        inAction = false;
    }
    public async void SaveResults(){
        if (reactionTime != 0){
            ReactionGameResult result = new ReactionGameResult(reactionTime: reactionTime);
            reactionResultList.Add(result);

            reactionTime = 0;
            await _httpClient.PostAsJsonAsync("api/reactiongameresults", result);
            
        }
    }

}