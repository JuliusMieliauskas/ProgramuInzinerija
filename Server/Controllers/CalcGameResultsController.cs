using Microsoft.AspNetCore.Mvc;
using Data;
using Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalcGameResultsController : ControllerBase
{
    private readonly IRepository<CalcGameResult> _repository;
    private readonly ILogger<CalcGameResultsController> _logger;

    public CalcGameResultsController(IRepository<CalcGameResult> repository, ILogger<CalcGameResultsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CalcGameResult>>> Get([FromQuery] bool sorted = false)
    {
        _logger.LogInformation("Getting all typing game results sorted by WPM to Errors ratio");
        var results = await _repository.GetAllAsync();
        if (sorted)
        {
            _logger.LogInformation("Sorting typing game results by WPM to Errors ratio");
            results = results.OrderByDescending(result => result).ToList();
        }
        return Ok(results);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CalcGameResult result)
    {
        _logger.LogInformation("Creating a new typing game result");
        await _repository.AddAsync(result);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }
}