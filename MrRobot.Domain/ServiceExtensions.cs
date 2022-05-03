using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MrRobot.Domain;

public static class ServiceExtensions
{
    public static void AddDomain(this IServiceCollection services) => services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
}