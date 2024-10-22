using Microsoft.EntityFrameworkCore;
using Shared;

namespace Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TypingGameResult> TypingGameResults { get; set; }

    public DbSet<ReactionGameResult> ReactionGameResults {get; set; }
}