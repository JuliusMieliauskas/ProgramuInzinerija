using Microsoft.AspNetCore.Mvc;
using Data;
using Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SampleTextController : ControllerBase
{

    private readonly ILogger<SampleTextController> _logger;

    public SampleTextController(ILogger<SampleTextController> logger)
    {
        _logger = logger;
    }

    [HttpGet("sample-text")]
    public async Task<IActionResult> GetSampleText()
    {
        try
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data/sample-text.txt");
            _logger.LogInformation("Loading sample text from {FilePath}", filePath);
            var text = await System.IO.File.ReadAllTextAsync(filePath);
            return Ok(text);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load sample text.");
            return StatusCode(500, "Error loading text.");
        }
    }
}