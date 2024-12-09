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

public class EasyCalculationIntegrationTests : EasyBase, IClassFixture<WebApplicationFactory<Program>>
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    public EasyCalculationIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        _httpClient = _client;
    }
    [Fact]
    public async Task CheckAnswer_InvalidInput_FormatException()
    {
        userAnswer = "ASDF";
        correctAnswer = 6;

        await CheckAnswer();
        
        var dataNew = await _client.GetFromJsonAsync<List<CalcGameResult>>("api/CalcGameResults");
        if (dataNew == null)
        {
            throw new ClientNullException("API GET returned null. Test:  ");
        }
        var dataNewExceptions = await _client.GetFromJsonAsync<List<ExceptionResult>>("api/ExceptionCatcher");
        if (dataNewExceptions == null)
        {
            throw new ClientNullException("API GET returned null. Test:  ");
        }


        Assert.Contains(dataNew, ourData => ourData.Id == 1);
        Assert.Contains(dataNew, ourData => ourData.Difficulty == "Easy");
        Assert.Contains(dataNew, ourData => ourData.Score == 0);
        
        Assert.Contains(dataNewExceptions, ourData => ourData.Id == 1);
        Assert.Contains(dataNewExceptions, ourData => ourData.Message == "Input was not in a correct format.");
        Assert.Contains(dataNewExceptions, ourData => ourData.InnerMessage == "The input string 'ASDF' was not in a correct format.");

        Assert.False(isCorrect);
        Assert.True(showResult);
    }
    [Fact]
    public async Task CheckAnswer_ValidInput_FivePlusFive()
    {
        userAnswer = "10";
        correctAnswer = 10;

        await CheckAnswer();
        
        var dataNew = await _client.GetFromJsonAsync<List<CalcGameResult>>("api/CalcGameResults");
        if (dataNew == null)
        {
            throw new ClientNullException("API GET returned null. Test:  ");
        }

        Assert.Contains(dataNew, ourData => ourData.Id == 1);
        Assert.Contains(dataNew, ourData => ourData.Difficulty == "Easy");
        Assert.Contains(dataNew, ourData => ourData.Score == 10);

        Assert.True(isCorrect);
        Assert.True(showResult);
    }

}