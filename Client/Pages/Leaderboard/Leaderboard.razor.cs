using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Shared;
using System.Net.Http.Json;
using System.Net.Http;

namespace Client.Pages;

public class LeaderboardBase : ComponentBase {
    [Inject]
    private HttpClient? _httpClient { get; set; }

    protected List<TypingGameResult> TypingGameResults { get; set; } = new List<TypingGameResult>();
    protected List<ReactionGameResult> ReactionGameResults { get; set; } = new List<ReactionGameResult>();
    protected List<CalcGameResult> CalcGameResults { get; set; } = new List<CalcGameResult>();
    protected override async Task OnInitializedAsync()
    {
        await LoadGameResults();
    }
    
    private async Task LoadGameResults()
    {
        if (_httpClient == null) throw new ClientNullException("HttpClient is null.");

        // Fetch Typing Game Results
        var typingResults = await _httpClient.GetFromJsonAsync<List<TypingGameResult>>("api/typinggameresults?sorted=true");
        if (typingResults != null)
        {
            TypingGameResults = typingResults;
        }

        // Fetch Reaction Game Results
        var reactionResults = await _httpClient.GetFromJsonAsync<List<ReactionGameResult>>("api/reactiongameresults");
        if (reactionResults != null)
        {
            ReactionGameResults = reactionResults.OrderBy(result => result.ReactionTime).ToList();
        }

        // Fetch Calc Game Results
        var calcResults = await HttpClient.GetFromJsonAsync<List<CalcGameResult>>("api/calcgameresults");
        if (calcResults != null)
        {
            CalcGameResults = calcResults;
        }
    }
}