using Microsoft.AspNetCore.Mvc;
using Data;
using Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MemoryGameResultsController : ControllerBase
{
    private readonly IRepository<MemoryGameResult> _repository;
    private readonly ILogger<MemoryGameResultsController> _logger;

    public MemoryGameResultsController(IRepository<MemoryGameResult> repository, ILogger<MemoryGameResultsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemoryGameResult>>> Get()
    {
        var results = await _repository.GetAllAsync();
        return Ok(results);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MemoryGameResult result)
    {
        _logger.LogInformation("Received result");
        _logger.LogInformation($"Adding result: {result.Missmatches}");
        await _repository.AddAsync(result);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }
}