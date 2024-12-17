using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Data;

public class MemoryGameResultsRepository : IRepository<MemoryGameResult> {
    private readonly AppDbContext _context;

    public MemoryGameResultsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MemoryGameResult>> GetAllAsync()
    {
        return await _context.MemoryGameResults.ToListAsync();
    }

    public async Task AddAsync(MemoryGameResult result)
    {
        _context.MemoryGameResults.Add(result);
        await _context.SaveChangesAsync();
    }
}