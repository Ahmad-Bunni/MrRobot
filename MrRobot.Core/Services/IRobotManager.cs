using MrRobot.Domain.Entities;
using MrRobot.Domain.Responses;

namespace MrRobot.Core.Services;

public interface IRobotsManager
{
    Task<CreateRobotDomainResponse> CreateAsync(Robot robot, CancellationToken cancellationToken);
    Task<RemoveRobotDomainStatus> RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<SendCommandsToRobotDomainResponse> SendCommandsToRobotByIdAsync(Guid id, string[] commands, CancellationToken cancellationToken);
    Task<SendCommandsToRobotsDomainResponse> SendCommandsToRobotsAsync(string[] commands, CancellationToken cancellationToken);
}
