using MediatR;
using MrRobot.Domain.DTOs;

namespace MrRobot.Domain.Contracts.Commands.Robots;

public record SendCommandsToRobotCommand : IRequest<SendCommandsToRobotCommandResult>
{
    public Guid Id { get; init; }
    public SendCommandsDto Payload { get; init; }

    public SendCommandsToRobotCommand(Guid id, SendCommandsDto payload)
    {
        Id = id;
        Payload = payload;
    }
}

public record SendCommandsToRobotCommandResult
{
    public RobotDto? Payload { get; init; }
    public SendCommandsToRobotCommandResultStatus Status { get; init; }
}

public enum SendCommandsToRobotCommandResultStatus
{
    Ok = 1,
    NotFound = 2,
    Failed = 3
}