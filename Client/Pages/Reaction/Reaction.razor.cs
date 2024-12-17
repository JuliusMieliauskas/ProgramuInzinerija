using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http.Json;
using System.Net.Http;
using Shared;

namespace Client.Pages;
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
    
    [Inject]
    protected HttpClient? _httpClient { get; set; }
    
    protected List<ReactionGameResult> reactionResultList = new List<ReactionGameResult>();

    protected void TestStart(){
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
    protected void TestFinish(){
        reactionTimer.Dispose();
        reactionTimer = new System.Timers.Timer(50);
        reactionTime = timePassed - reactionSpringUp;
        timePassed = 0;
        springUp = false;
        inAction = false;
    }
    protected async Task SaveResults(){
        try
        {
            if (_httpClient == null) throw new ClientNullException();
        }
        catch(ClientNullException){
            commencingText = "Server Error, results cannot be saved!";
            return;
        }
        if (reactionTime != 0){
            ReactionGameResult result = new ReactionGameResult(reactionTime: reactionTime);
            reactionResultList.Add(result);

            reactionTime = 0;
            await _httpClient.PostAsJsonAsync("api/reactiongameresults", result);
            
        }
    }

}