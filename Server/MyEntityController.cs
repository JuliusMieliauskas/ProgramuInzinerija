using Microsoft.AspNetCore.Mvc;
using MyApp.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Controllers;

[ApiController]
[Route("[controller]")]
public class MyEntityController : ControllerBase
{
    private readonly AppDbContext _context;

    public MyEntityController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MyEntity entity)
    {
        _context.MyEntities.Add(entity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Post), new { id = entity.Id }, entity);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MyEntity>>> Get()
    {
        var entities = await _context.MyEntities.ToListAsync();
        return Ok(entities);
    }
}