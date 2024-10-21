using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MyApp.Shared;
using System.Net.Http.Json;
using System.Net.Http;

namespace MyApp.Pages;

public class LeaderboardBase : ComponentBase {
    [Inject]
    private HttpClient HttpClient { get; set; }

    protected List<TypingGameResult> TypingGameResults { get; set; } = new List<TypingGameResult>();
    protected List<ReactionGameResult> ReactionGameResults { get; set; } = new List<ReactionGameResult>();

    protected override async Task OnInitializedAsync()
    {
        await LoadGameResults();
    }
    
    private async Task LoadGameResults()
    {
        // Fetch Typing Game Results
        var typingResults = await HttpClient.GetFromJsonAsync<List<TypingGameResult>>("api/typinggameresults");
        if (typingResults != null)
        {
            TypingGameResults = typingResults.OrderByDescending(result => result.WordsPerMinute).ToList();
        }

        // Fetch Reaction Game Results
        var reactionResults = await HttpClient.GetFromJsonAsync<List<ReactionGameResult>>("api/reactiongameresults");
        if (reactionResults != null)
        {
            ReactionGameResults = reactionResults.OrderBy(result => result.ReactionTime).ToList();
        }
    }
}