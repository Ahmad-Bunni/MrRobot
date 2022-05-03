using MrRobot.Domain.Entities;

namespace MrRobot.Domain.Responses;
public record SendCommandsToRobotDomainResponse
{
    public Robot? Robot { get; init; }
    public SendCommandsToRobotDomainStatus Status { get; init; }
}
