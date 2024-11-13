using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Data;
using Shared;

namespace Extensions;

public static class ServiceExtensions
{
    // Extension method to add services to the container
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("MyDatabase"));

        services.AddScoped<IRepository<ReactionGameResult>, ReactionGameResultsRepository>();
        services.AddScoped<IRepository<TypingGameResult>, TypingGameResultRepository>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
        });

        services.AddControllers();

        return services;
    }
}