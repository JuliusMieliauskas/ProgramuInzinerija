using Microsoft.AspNetCore.Mvc;
using MyApp.Data;
using MyApp.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TypingGameResultsController : ControllerBase
{
    private readonly TypingGameResultRepository _repository;
    private readonly ILogger<TypingGameResultsController> _logger;

    public TypingGameResultsController(TypingGameResultRepository repository, ILogger<TypingGameResultsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TypingGameResult>>> Get()
    {
        _logger.LogInformation("Getting all typing game results");
        var results = await _repository.GetAllAsync();
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TypingGameResult>> Get(int id)
    {
        var result = await _repository.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TypingGameResult result)
    {
        _logger.LogInformation("Creating a new typing game result");
        await _repository.AddAsync(result);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] TypingGameResult result)
    {
        if (id != result.Id)
        {
            return BadRequest();
        }

        await _repository.UpdateAsync(result);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}