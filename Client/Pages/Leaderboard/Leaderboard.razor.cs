using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Shared;
using System.Net.Http.Json;
using System.Net.Http;

namespace Client.Pages;

public class LeaderboardBase : ComponentBase {
    [Inject]
    private HttpClient _httpClient { get; set; } = null!;
    protected List<TypingGameResult> TypingGameResults { get; set; } = new List<TypingGameResult>();
    protected List<ReactionGameResult> ReactionGameResults { get; set; } = new List<ReactionGameResult>();
    protected List<CalcGameResult> CalcGameResults { get; set; } = new List<CalcGameResult>();
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
            var calcResults = await _httpClient.GetFromJsonAsync<List<CalcGameResult>>("api/calcgameresults");
            if (calcResults != null)
            {
                CalcGameResults = calcResults;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching Calculation Game results: {ex.Message}");
        }
    }

}