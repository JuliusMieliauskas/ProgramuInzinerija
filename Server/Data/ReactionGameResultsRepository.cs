using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Data;

public class ReactionGameResultsRepository : IRepository<ReactionGameResult> {
    private readonly AppDbContext _context;

    public ReactionGameResultsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReactionGameResult>> GetAllAsync()
    {
        return await _context.ReactionGameResults.ToListAsync();
    }

    public async Task AddAsync(ReactionGameResult result)
    {
        _context.ReactionGameResults.Add(result);
        await _context.SaveChangesAsync();
    }
}