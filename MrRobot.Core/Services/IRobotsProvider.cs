using MrRobot.Domain.Entities;

namespace MrRobot.Core.Services;
public interface IRobotsProvider
{
    Task<IEnumerable<Robot>> GetRobotsAsync(CancellationToken cancellationToken);
}
