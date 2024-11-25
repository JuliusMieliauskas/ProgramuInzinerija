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
using Shared;
using Data;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Moq;
namespace Tests;

public class TypingGameIntegrationTests : TypingBase, IClassFixture<WebApplicationFactory<Program>>
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    public TypingGameIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        _httpClient = _client;
    }

    [Fact]
    public async Task  OnInitializedAsync_ValueSetTest()
    {
        _httpClient = _client;
        var mockLogger = new Mock<ILogger<TypingBase>>();
        _logger = mockLogger.Object;

        var response = await _client.GetAsync("api/sampletext/sample-text");
    
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        await OnInitializedAsync();
        Assert.NotNull(GameTimer);
        Assert.Equal("Breakfast procuring nay end happiness allowance assurance frankness. Met simplicity nor difficulty unreserved who. Entreaties mr conviction dissimilar me astonished estimating cultivated. On no applauded exquisite my additions. Pronounce add boy estimable nay suspected. You sudden nay elinor thirty esteem temper. Quiet leave shy you gay off asked large style. Oh to talking improve produce in limited offices fifteen an. Wicket branch to answer do we. Place are decay men hours tiled. If or of ye throwing friendly required. Marianne interest in exertion as. Offering my branched confined oh dashwood.",
         SampleText);
    }

    [Fact]
    public async Task  SaveTypingGameResultsAsync_()
    {
        _httpClient = _client;
        var mockLogger = new Mock<ILogger<TypingBase>>();
        _logger = mockLogger.Object;

        WPM = 50;
        ErrorCount = 2;

        await SaveTypingGameResultsAsync();

        var dataNew = await _client.GetFromJsonAsync<List<TypingGameResult>>("api/TypingGameResults");
        if (dataNew == null)
        {
            throw new ClientNullException("API GET returned null. Test:  ", "GetPost_TypingResults_PerfectRun");
        }

        Assert.Contains(dataNew, ourData => ourData.Id == 1);
        Assert.Contains(dataNew, ourData => ourData.Errors == 2);
        Assert.Contains(dataNew, ourData => ourData.Status == TypingGameStatus.NotPerfectRun);
        Assert.Contains(dataNew, ourData => ourData.WordsPerMinute == 50);
    }
}