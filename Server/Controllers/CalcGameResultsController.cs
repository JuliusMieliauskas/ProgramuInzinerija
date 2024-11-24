using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class CalcGameResultsController : ControllerBase
{
    private static readonly List<CalcGameResult> CalcGameResults = new();

    [HttpGet]
    public ActionResult<List<CalcGameResult>> GetResults()
    {
        var sortedResults = CalcGameResults
            .OrderByDescending(result => result.CorrectAnswers)
            .ThenByDescending(result => result.TotalRounds)
            .ToList();
        return Ok(sortedResults);
    }

    [HttpPost]
    public ActionResult AddResult(CalcGameResult result)
    {
        result.Id = CalcGameResults.Count > 0 ? CalcGameResults.Max(r => r.Id) + 1 : 1;
        CalcGameResults.Add(result);
        return Ok();
    }
}
