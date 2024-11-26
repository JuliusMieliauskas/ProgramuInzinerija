using System;
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
using System.Data.Common;
using MyApp.Pages.Calculator;

namespace Tests;

public class CalcGameControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
{

    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    public CalcGameControllerIntegrationTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    [Fact]
    public async Task GetPost_CalcResults()
    {
        var response = await _client.GetAsync("/api/CalcGameResults");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        

        CalcGameResult result = new CalcGameResult
        (
            difficulty: "Easy",
            score: 10,
            id: 2
        );

        var postResponse = await _client.PostAsJsonAsync("/api/CalcGameResults", result);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        await _client.PostAsJsonAsync("/api/CalcGameResults", result);

        var data = JsonConvert.DeserializeObject<IEnumerable<CalcGameResult>>(await response.Content.ReadAsStringAsync());
        var dataNew = await _client.GetFromJsonAsync<List<CalcGameResult>>("api/CalcGameResults");
        if (dataNew == null)
        {
            throw new ClientNullException("API GET returned null. Test:  ", "GetPost_CalcResults");
        }
        Assert.Contains(dataNew, ourResult => ourResult.Difficulty == "Easy");
        Assert.Contains(dataNew, ourResult => ourResult.Id == 2);
        Assert.Contains(dataNew, ourResult => ourResult.Score == 10);
    }
}
