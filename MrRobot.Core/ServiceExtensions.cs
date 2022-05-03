using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MrRobot.Core.Services;
using MrRobot.Infrastructure;
using System.Reflection;

namespace MrRobot.Core;
public static class ServiceExtensions
{
    public static void AddCore(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddDbContext<MrRobotDbContext>(opt => opt.UseInMemoryDatabase(nameof(MrRobot)));
        services.AddScoped<IRobotsManager, RobotsManager>();
        services.AddScoped<IRobotsProvider, RobotsProvider>();
    }
}