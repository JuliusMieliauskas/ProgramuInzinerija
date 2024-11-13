using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Data;

public class TypingGameResultRepository : IRepository<TypingGameResult> {
    private readonly AppDbContext _context;

    public TypingGameResultRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TypingGameResult>> GetAllAsync()
    {
        return await _context.TypingGameResults.ToListAsync();
    }

    public async Task AddAsync(TypingGameResult result)
    {
        _context.TypingGameResults.Add(result);
        await _context.SaveChangesAsync();
    }
}