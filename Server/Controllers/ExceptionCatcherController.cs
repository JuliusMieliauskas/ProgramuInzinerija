using Microsoft.AspNetCore.Mvc;
using Data;
using Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExceptionCatcherController : ControllerBase
{
    private readonly IRepository<ExceptionResult> _repository;

    public ExceptionCatcherController(IRepository<ExceptionResult> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExceptionResult>>> Get()
    {
        var results = await _repository.GetAllAsync();
        return Ok(results);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ExceptionResult result)
    {
        await _repository.AddAsync(result);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }
}