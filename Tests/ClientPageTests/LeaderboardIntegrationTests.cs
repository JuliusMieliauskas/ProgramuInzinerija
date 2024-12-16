using Client.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components;
using Shared;
using Data;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Components.Web;
namespace Tests;

public class LeaderboardIntegrationTests : LeaderboardBase, IClassFixture<WebApplicationFactory<Program>>
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    public LeaderboardIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        _httpClient = _client;
    }

    [Fact]
    public async Task OnInitializedAsync_LoadsGameResults()
    {
        var reactionResult = new ReactionGameResult(reactionTime: 2.1);
        var typingResult = new TypingGameResult(wordsPerMinute: 70, errors: 0);
        var calcResult = new CalcGameResult
        (
            difficulty: "Easy",
            score: 10,
            id: 2
        );
        var memoryResult = new MemoryGameResult(missmatches: 10);

        await _client.PostAsJsonAsync("/api/ReactionGameResults", reactionResult);
        await _client.PostAsJsonAsync("/api/CalcGameResults", calcResult);
        await _client.PostAsJsonAsync("/api/TypingGameResults", typingResult);
        await _client.PostAsJsonAsync("/api/MemoryGameResults", memoryResult);



        await OnInitializedAsync();

        Assert.Contains(ReactionGameResults, data => data.Id == 1);
        Assert.Contains(ReactionGameResults, data => data.ReactionTime == 2.1);

        Assert.Contains(TypingGameResults, data => data.Id == 1);
        Assert.Contains(TypingGameResults, data => data.Errors == 0);
        Assert.Contains(TypingGameResults, data => data.WordsPerMinute == 70);

        Assert.Contains(CalcGameResults, data => data.Id == 2);
        Assert.Contains(CalcGameResults, data => data.Difficulty == "Easy");
        Assert.Contains(CalcGameResults, data => data.Score == 10);

        Assert.Contains(MemoryGameResults, data => data.Id == 1);
        Assert.Contains(MemoryGameResults, data => data.Missmatches == 10);
    }

    [Fact]
    public async void OnInitializedAsync_HttpClientNull()
    {
        _httpClient = null;
        await OnInitializedAsync();
        Assert.Equal("Error! _httpClient is null.", infoText);
        // await Assert.ThrowsAsync<ClientNullException>(() => OnInitializedAsync());
    }
}