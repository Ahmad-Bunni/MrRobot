using MrRobot.Domain.Entities;

namespace MrRobot.Domain.Responses;
public record SendCommandsToRobotsDomainResponse
{
    public IEnumerable<Robot> Robots { get; init; } = null!;
    public SendCommandsToRobotsDomainStatus Status { get; init; }
}
