using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Shared;
using System.Net.Http.Json;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;

namespace Client.Pages;

public class LeaderboardBase : ComponentBase
{
    [Inject]
    private HttpClient _httpClient { get; set; } = null!;
    protected List<TypingGameResult> TypingGameResults { get; set; } = new List<TypingGameResult>();
    protected List<ReactionGameResult> ReactionGameResults { get; set; } = new List<ReactionGameResult>();
    protected List<CalcGameResult> CalcGameResults { get; set; } = new List<CalcGameResult>();
    protected List<MemoryGameResult> MemoryGameResults { get; set; } = new List<MemoryGameResult>();
    protected String infoText = "";
    protected override async Task OnInitializedAsync()
    {
        try
        {
            await LoadGameResults();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading results: {ex.Message}");
        }
    }


    private async Task LoadGameResults()
    {
        try
        {
            if (_httpClient == null) throw new ClientNullException("_httpClient is null.");
        }
        catch (ClientNullException e)
        {
            infoText = "Error! " + e.Message; 
            return;
        }
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
        var calcResults = await _httpClient.GetFromJsonAsync<List<CalcGameResult>>("api/calcgameresults");
        if (calcResults != null)
        {
            CalcGameResults = calcResults;
        }

        // Fetch Reaction Game Results
        
        // Fetch Memory Game Results
        /*
        var memoryResults = await _httpClient.GetFromJsonAsync<List<MemoryGameResult>>("api/memorygameresults");
        if (memoryResults != null)
        {
            MemoryGameResults = memoryResults.OrderBy(result => result.Missmatches).ToList();
        }
        
    }
}