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
public class ReactionGameIntegrationTests : ReactionBase, IClassFixture<WebApplicationFactory<Program>>
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    public ReactionGameIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        _httpClient = _client;
    }

    [Fact]
    public async Task SaveResults_WhenHttpClientNull()
    {
        _httpClient = null;

        await SaveResults();

        Assert.Equal("Server Error, results cannot be saved!", commencingText);
    }
    [Fact]
    public async Task SaveResults_ValidResults_EqualsZero()
    {
        reactionTime = 0;
        await SaveResults();

        var dataNew = await _client.GetFromJsonAsync<List<ReactionGameResult>>("api/ReactionGameResults");
        if (dataNew == null)
        {
            throw new ClientNullException("API GET returned null. Test:  ");
        }

        Assert.Equal(0, reactionTime);
        Assert.Contains(dataNew, data => data.ReactionTime != 0);
    }
    [Fact]
    public async Task SaveResults_ValidResults_NotZero()
    {
        reactionTime = 2.5;
        await SaveResults();

        var dataNew = await _client.GetFromJsonAsync<List<ReactionGameResult>>("api/ReactionGameResults");
        if (dataNew == null)
        {
            throw new ClientNullException("API GET returned null. Test:  ");
        }

        Assert.Equal(0, reactionTime);
        Assert.Contains(dataNew, data => data.Id == 1);
        Assert.Contains(dataNew, data => data.ReactionTime == 2.5);
    }
}
