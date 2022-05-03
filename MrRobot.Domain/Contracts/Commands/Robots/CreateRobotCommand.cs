using MediatR;
using MrRobot.Domain.DTOs;

namespace MrRobot.Domain.Contracts.Commands.Robots;

public record CreateRobotCommand : IRequest<CreateRobotCommandResult>
{
    public CreateRobotDto Payload { get; init; }

    public CreateRobotCommand(CreateRobotDto payload)
    {
        Payload = payload;
    }
}

public record CreateRobotCommandResult
{
    public RobotDto? Payload { get; init; }
    public CreateRobotCommandResultStatus Status { get; init; }
}

public enum CreateRobotCommandResultStatus
{
    Ok = 1,
    AlreadyExists = 2,
    Failed = 3
}