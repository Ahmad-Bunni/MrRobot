using MediatR;

namespace MrRobot.Domain.Contracts.Commands.Robots;

public record RemoveRobotCommand : IRequest<RemoveRobotCommandResult>
{
    public Guid Id { get; init; }

    public RemoveRobotCommand(Guid id)
    {
        Id = id;
    }
}

public record RemoveRobotCommandResult
{
    public RemoveRobotCommandResultStatus Status { get; init; }
}

public enum RemoveRobotCommandResultStatus
{
    Ok = 1,
    NotFound = 2,
    Failed = 3
}