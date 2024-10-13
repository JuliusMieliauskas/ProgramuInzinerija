using Microsoft.EntityFrameworkCore;
using MyApp.Shared;

namespace MyApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TypingGameResult> TypingGameResults { get; set; }
}