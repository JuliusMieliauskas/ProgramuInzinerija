using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Shared;
using Data;
using System.Net.Http.Json;

namespace Tests;

public class ControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{

    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    public ControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    [Fact]
    public async Task GetPost_ReactionResults()
    {

        var response = await _client.GetAsync("/api/ReactionGameResults");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        ReactionGameResult result = new ReactionGameResult(reactionTime: 2.1);
        await _client.PostAsJsonAsync("/api/ReactionGameResults", result);

        var data = JsonConvert.DeserializeObject<IEnumerable<ReactionGameResult>>(await response.Content.ReadAsStringAsync());
        var dataNew = await _client.GetFromJsonAsync<List<ReactionGameResult>>("api/reactiongameresults");
        if (dataNew == null)
        {
            throw new ClientNullException("API GET returned null. Test:  ", "GetPost_TypingResults_PerfectRun");
        }
        Assert.Contains(dataNew, ourResult => ourResult.ReactionTime == 2.1);
        Assert.Contains(dataNew, ourResult => ourResult.Id == 1);
    }
    [Fact]
    public async Task GetPost_TypingResults_RunWithErrors()
    {

        var response = await _client.GetAsync("/api/TypingGameResults");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        TypingGameResult result = new TypingGameResult(wordsPerMinute: 50, errors: 4);
        await _client.PostAsJsonAsync("/api/TypingGameResults", result);

        var dataNew = await _client.GetFromJsonAsync<List<TypingGameResult>>("api/TypingGameResults");
        if (dataNew == null)
        {
            throw new ClientNullException("API GET returned null. Test:  ", "GetPost_TypingResults_PerfectRun");
        }
        Assert.Contains(dataNew, ourResult => ourResult.WordsPerMinute == 50);
        Assert.Contains(dataNew, ourResult => ourResult.Id == 1);
        Assert.Contains(dataNew, ourResult => ourResult.Errors == 4);
        Assert.Contains(dataNew, ourResult => ourResult.Status == TypingGameStatus.NotPerfectRun);
    }
    [Fact]
    public async Task GetPost_TypingResults_PerfectRun()
    {

        var response = await _client.GetAsync("/api/TypingGameResults");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        TypingGameResult result = new TypingGameResult(wordsPerMinute: 70, errors: 0);
        await _client.PostAsJsonAsync("/api/TypingGameResults", result);

        var dataNew = await _client.GetFromJsonAsync<List<TypingGameResult>>("api/TypingGameResults");
        if (dataNew == null)
        {
            throw new ClientNullException("API GET returned null. Test:  ", "GetPost_TypingResults_PerfectRun");
        }
        Assert.Contains(dataNew, ourResult => ourResult.WordsPerMinute == 70);
        Assert.Contains(dataNew, ourResult => ourResult.Id == 1);
        Assert.Contains(dataNew, ourResult => ourResult.Errors == 0);
        Assert.Contains(dataNew, ourResult => ourResult.Status == TypingGameStatus.PerfectRun);
    }
}
