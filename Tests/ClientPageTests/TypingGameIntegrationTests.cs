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
    public async Task OnInitializedAsync_ValueSetTest()
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
    public async Task OnInitializedAsync_ThrowsExceptionLogger()
    {

        await OnInitializedAsync();

        Assert.Equal("Error! Object reference not set to an instance of an object.", infoText);

    }

    [Fact]
    public async Task OnInitializedAsync_InitializesTimer()
    {
        _httpClient = _client;
        var mockLogger = new Mock<ILogger<TypingBase>>();
        _logger = mockLogger.Object;

        await OnInitializedAsync();

        Assert.NotNull(GameTimer);
        Assert.Equal(1000, GameTimer.Interval);
    }

    [Fact]
    public async Task OnInitializedAsync_ThrowsExceptionClient()
    {
        var mockLogger = new Mock<ILogger<TypingBase>>();
        _logger = mockLogger.Object;
        _httpClient = null;

        await OnInitializedAsync();

        Assert.Equal("Error! _httpClient is null!", infoText);
    }

    // [Fact]
    // public async Task StartGame_ShouldInitializeGameStateAndSetFocus()
    // {
    //     GameTimer = new System.Timers.Timer(1000); 

    //     // Arrange
    //     var mockTextAreaReference = new ElementReference();
    //     textAreaReference = mockTextAreaReference;

    //     // Act
    //     await StartGame();

    //     // Assert
    //     Assert.Equal("", UserInput);
    //     Assert.Equal(10, TimeRemaining);
    //     Assert.Equal(0, WPM);
    //     Assert.Equal(0, ErrorCount);
    //     Assert.True(GameStarted);
        

    // }

    [Fact]
    public async Task EndGame_AssertValues()
    {
        var mockLogger = new Mock<ILogger<TypingBase>>();
        _logger = mockLogger.Object;
        _httpClient = _client;



        GameTimer = new System.Timers.Timer(1000); 
        WPM = 100;
        ErrorCount = 0;

        await EndGame();

        Assert.False(GameTimer.Enabled);
        Assert.False(GameStarted);
    }


    [Fact]
    public async Task SaveTypingGameResultsAsync_ValidResults_ErrorCountAbove0()
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
    [Fact]
    public async Task SaveTypingGameResultsAsync_ValidResults_ErrorCountEquals0()
    {
        _httpClient = _client;
        var mockLogger = new Mock<ILogger<TypingBase>>();
        _logger = mockLogger.Object;

        WPM = 50;
        ErrorCount = 0;

        await SaveTypingGameResultsAsync();

        var dataNew = await _client.GetFromJsonAsync<List<TypingGameResult>>("api/TypingGameResults");
        if (dataNew == null)
        {
            throw new ClientNullException("API GET returned null. Test:  ", "GetPost_TypingResults_PerfectRun");
        }

        Assert.Contains(dataNew, ourData => ourData.Id == 1);
        Assert.Contains(dataNew, ourData => ourData.Errors == 0);
        Assert.Contains(dataNew, ourData => ourData.Status == TypingGameStatus.PerfectRun);
        Assert.Contains(dataNew, ourData => ourData.WordsPerMinute == 50);
    }
    [Fact]
    public async Task SaveTypingGameResultsAsync_LoggerNull()
    {
        await SaveTypingGameResultsAsync();
        Assert.Equal("Error! Object reference not set to an instance of an object.", infoText);
    }
    [Fact]
    public async Task SaveTypingGameResultsAsync_HttpClientNull()
    {
        _httpClient = null;
        var mockLogger = new Mock<ILogger<TypingBase>>();
        _logger = mockLogger.Object;
        await SaveTypingGameResultsAsync();
        Assert.Equal("Error! _httpClient is null!", infoText);
    }

    [Fact]
    public void HandleKeyDown_GameStarted()
    {
        GameStarted = true;
        UserInput = "hello worls";
        SampleText = "hello world";
        WPM = 0;
        ErrorCount = 0;
        TimeRemaining = 20;

        HandleKeyDown(new KeyboardEventArgs{
            Key = "A",
            Code = "65"
        });

        Assert.Equal(13, WPM);
        Assert.Equal(1, ErrorCount);
    }
    [Fact]
    public void HandleKeyDown_GameNotStarted()
    {
        GameStarted = false;
        UserInput = "hello worls";
        SampleText = "hello world";
        WPM = 0;
        ErrorCount = 0;
        TimeRemaining = 20;

        HandleKeyDown(new KeyboardEventArgs{
            Key = "A",
            Code = "65"
        });

        Assert.Equal(0, WPM);
        Assert.Equal(0, ErrorCount);
    }
}