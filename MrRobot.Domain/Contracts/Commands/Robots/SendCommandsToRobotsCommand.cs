using MediatR;
using MrRobot.Domain.DTOs;

namespace MrRobot.Domain.Contracts.Commands.Robots;

public record SendCommandsToRobotsCommand : IRequest<SendCommandsToRobotsCommandResult>
{
    public SendCommandsDto Payload { get; init; }

    public SendCommandsToRobotsCommand(SendCommandsDto payload)
    {
        Payload = payload;
    }
}

public record SendCommandsToRobotsCommandResult
{
    public IEnumerable<RobotDto> Payload { get; init; } = null!;
    public SendCommandsToRobotsCommandResultStatus Status { get; init; }
}

public enum SendCommandsToRobotsCommandResultStatus
{
    Ok = 1,
    NotFound = 2,
    Failed = 3
}