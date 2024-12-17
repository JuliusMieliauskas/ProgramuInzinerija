using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using Data;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CalcGameResultsController : ControllerBase
{
    private readonly AppDbContext _context;

    public CalcGameResultsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult> AddResult([FromBody] CalcGameResult result)
    {
        if (result == null) return BadRequest("Invalid result data.");

        try
        {
            _context.CalcGameResults.Add(result);
            await _context.SaveChangesAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<CalcGameResult>>> GetResults()
    {
        var sortedResults = await _context.CalcGameResults
            .OrderByDescending(result => result.CorrectAnswers)
            .ThenByDescending(result => result.TotalRounds)
            .ToListAsync();

        return Ok(sortedResults);
    }
}