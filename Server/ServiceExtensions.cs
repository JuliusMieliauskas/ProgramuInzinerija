using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;

namespace MyApp.Extensions;

public static class ServiceExtensions
{
    // Extension method to add services to the container
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("MyDatabase"));

        services.AddScoped<TypingGameResultRepository>();
        services.AddScoped<ReactionGameResultsRepository>();

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