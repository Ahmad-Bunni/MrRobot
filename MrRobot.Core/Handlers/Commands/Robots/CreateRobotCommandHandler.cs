using MediatR;
using MrRobot.Core.Services;
using MrRobot.Domain.Contracts.Commands.Robots;
using MrRobot.Domain.Responses;

namespace MrRobot.Core.Handlers.Commands.Robots;

public class CreateRobotCommandHandler : IRequestHandler<CreateRobotCommand, CreateRobotCommandResult>
{
    private readonly IRobotsManager _robotsManager;

    public CreateRobotCommandHandler(IRobotsManager robotsManager)
    {
        _robotsManager = robotsManager;
    }

    public async Task<CreateRobotCommandResult> Handle(CreateRobotCommand request, CancellationToken cancellationToken)
    {
        var robot = request.Payload.ToModel();

        var result = await _robotsManager.CreateAsync(robot, cancellationToken);

        return new CreateRobotCommandResult
        {
            Payload = result.Robot?.ToDto(),
            Status = MapResult(result.Status),
        };
    }

    private static CreateRobotCommandResultStatus MapResult(CreateRobotDomainStatus status) => status switch
    {
        CreateRobotDomainStatus.Ok => CreateRobotCommandResultStatus.Ok,
        CreateRobotDomainStatus.AlreadyExists => CreateRobotCommandResultStatus.AlreadyExists,
        CreateRobotDomainStatus.AggregateNotFound => CreateRobotCommandResultStatus.Failed,
        CreateRobotDomainStatus.Failed => CreateRobotCommandResultStatus.Failed,
        _ => throw new ArgumentOutOfRangeException(nameof(status), $"Not expected direction value: {status}")
    };
}
