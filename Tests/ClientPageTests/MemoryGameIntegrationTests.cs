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
public class MemoryGameIntegrationTests : MemoryBase, IClassFixture<WebApplicationFactory<Program>>
{
    //TODO add tests
}