using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public async Task<ActionResult<List<CalcGameResult>>> GetResults()
    {
        var sortedResults = _context.CalcGameResults
            .OrderByDescending(result => result.CorrectAnswers)
            .ThenByDescending(result => result.TotalRounds)
            .ToList();
        return Ok(sortedResults);
    }

    [HttpPost]
    public async Task<ActionResult> AddResult([FromBody] CalcGameResult result)
    {
        if (result == null) return BadRequest("Invalid result data.");

        _context.CalcGameResults.Add(result);
        await _context.SaveChangesAsync();
        return Ok(result);
    }
}