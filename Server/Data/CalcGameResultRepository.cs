using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Data;

public class CalcGameResultRepository : IRepository<CalcGameResult> {
    private readonly AppDbContext _context;

    public CalcGameResultRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CalcGameResult>> GetAllAsync()
    {
        return await _context.CalcGameResults.ToListAsync();
    }

    public async Task AddAsync(CalcGameResult result)
    {
        _context.CalcGameResults.Add(result);
        await _context.SaveChangesAsync();
    }

}