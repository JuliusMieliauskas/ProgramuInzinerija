using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Data;
using Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomServices(); // Use the extension method to add all services

var app = builder.Build();

app.UseCors("AllowAllOrigins"); 

app.MapControllers();

app.Run();