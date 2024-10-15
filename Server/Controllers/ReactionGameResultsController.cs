using Microsoft.AspNetCore.Mvc;
using MyApp.Data;
using MyApp.Shared;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReactionGameResultsController : ControllerBase
{
    ReactionGameResultStream _stream;
    public ReactionGameResultsController()
    {
        _stream = new ReactionGameResultStream();
    }
    // [HttpGet]
    // public async Task<List<ReactionGameResult>> GetEvent()
    // {
    //     var results = await _stream.GetAllAsync();
    //     return results;
    // }

    [HttpGet]
    public async Task<ActionResult<List<ReactionGameResult>>> Get()
    {
        
        return Ok(await _stream.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ReactionGameResult result){
        await _stream.AddAsync(result);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }
}