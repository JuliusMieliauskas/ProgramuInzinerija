using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Data;
using Extensions;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCustomServices();

        var app = builder.Build();

        app.UseCors("AllowAllOrigins");

        app.MapControllers();

        app.Run();
    }
}