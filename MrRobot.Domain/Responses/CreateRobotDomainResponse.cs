using MrRobot.Domain.Entities;

namespace MrRobot.Domain.Responses;
public record CreateRobotDomainResponse
{
    public Robot? Robot { get; init; }
    public CreateRobotDomainStatus Status { get; init; }
}
