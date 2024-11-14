
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApiIntegrationTesting.IntegrationTests.ControllerTests;

// public class ReactionGameResultsControllerTests : IDisposable
// {
//     private CustomWebApplicationFactory _factory;
//     private HttpClient _client;

//     public ReactionGameResultsControllerTests()
//     {
//         _factory = new CustomWebApplicationFactory();
//         _client = _factory.CreateClient();
//     }

//     [Fact]
//     public async Task testName()
//     {
//         var response = await _client.GetAsync("/api/ReactionGameResults");
//         Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//     }


//     public void Dispose()
//     {
//         _client.Dispose();
//         _factory.Dispose();
//     }
// }