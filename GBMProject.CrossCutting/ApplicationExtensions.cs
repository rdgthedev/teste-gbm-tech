using GBMProject.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GBMProject.CrossCutting;

public static class ApplicationExtensions
{
    public static void UseScopeToGenerateMigrations(this WebApplication app)
    {
        try
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<GbmProjectDbContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}