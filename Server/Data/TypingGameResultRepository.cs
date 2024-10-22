using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Data;

public class TypingGameResultRepository
{
    private readonly AppDbContext _context;

    public TypingGameResultRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TypingGameResult>> GetAllAsync()
    {
        return await _context.TypingGameResults.ToListAsync();
    }

    public async Task<TypingGameResult> GetByIdAsync(int id)
    {
        return await _context.TypingGameResults.FindAsync(id);
    }

    public async Task AddAsync(TypingGameResult result)
    {
        _context.TypingGameResults.Add(result);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TypingGameResult result)
    {
        _context.Entry(result).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var result = await _context.TypingGameResults.FindAsync(id);
        if (result != null)
        {
            _context.TypingGameResults.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
}