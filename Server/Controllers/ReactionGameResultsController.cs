using Microsoft.AspNetCore.Mvc;
using Data;
using Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReactionGameResultsController : ControllerBase
{
    private readonly ReactionGameResultsRepository _repository;
    private readonly ILogger<ReactionGameResultsController> _logger;

    public ReactionGameResultsController(ReactionGameResultsRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReactionGameResult>>> Get()
    {
        var results = await _repository.GetAllAsync();
        return Ok(results);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ReactionGameResult result)
    {
        await _repository.AddAsync(result);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }
}