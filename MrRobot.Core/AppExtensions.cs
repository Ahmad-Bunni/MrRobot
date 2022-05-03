using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MrRobot.Infrastructure;

namespace MrRobot.Core;
public static class AppExtensions
{
    public static async Task InitializeDatabase(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        using var db = scope.ServiceProvider.GetService<MrRobotDbContext>();

        if (db == null)
        {
            throw new Exception("database context instance not found");
        }

        await db.Database.EnsureCreatedAsync();
    }
}
