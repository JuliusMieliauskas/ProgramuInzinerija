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
        return Ok(CalcGameResults.OrderByDescending(result => result.Score).ToList());
    }

    [HttpPost]
    public ActionResult AddResult(CalcGameResult result)
    {
        result.Id = CalcGameResults.Count > 0 ? CalcGameResults.Max(r => r.Id) + 1 : 1;
        CalcGameResults.Add(result);
        return Ok();
    }
}
