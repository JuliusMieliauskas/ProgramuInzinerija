using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Shared;
using System.Net.Http.Json;
using System.Net.Http;

namespace Client.Pages;

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
        var typingResults = await HttpClient.GetFromJsonAsync<List<TypingGameResult>>("api/typinggameresults?sorted=true");
        if (typingResults != null)
        {
            TypingGameResults = typingResults;
        }

        // Fetch Reaction Game Results
        var reactionResults = await HttpClient.GetFromJsonAsync<List<ReactionGameResult>>("api/reactiongameresults");
        if (reactionResults != null)
        {
            ReactionGameResults = reactionResults.OrderBy(result => result.ReactionTime).ToList();
        }
    }
}