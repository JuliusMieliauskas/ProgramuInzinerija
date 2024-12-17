using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared;


namespace Data;

public class ExceptionResultRepository : IRepository<ExceptionResult> {
    private readonly AppDbContext _context;

    public ExceptionResultRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExceptionResult>> GetAllAsync()
    {
        return await _context.ExceptionResults.ToListAsync();
    }

    public async Task AddAsync(ExceptionResult result)
    {
        _context.ExceptionResults.Add(result);
        await _context.SaveChangesAsync();
    }
}