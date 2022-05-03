using MediatR;
using MrRobot.Core.Services;
using MrRobot.Domain.Contracts.Commands.Robots;
using MrRobot.Domain.Responses;

namespace MrRobot.Core.Handlers.Commands.Robots;

public class RemoveRobotCommandHandler : IRequestHandler<RemoveRobotCommand, RemoveRobotCommandResult>
{
    private readonly IRobotsManager _robotsManager;

    public RemoveRobotCommandHandler(IRobotsManager robotsManager)
    {
        _robotsManager = robotsManager;
    }

    public async Task<RemoveRobotCommandResult> Handle(RemoveRobotCommand request, CancellationToken cancellationToken)
    {

        var result = await _robotsManager.RemoveByIdAsync(request.Id, cancellationToken);

        return new RemoveRobotCommandResult
        {
            Status = MapResult(result),
        };
    }

    private static RemoveRobotCommandResultStatus MapResult(RemoveRobotDomainStatus status) => status switch
    {
        RemoveRobotDomainStatus.Ok => RemoveRobotCommandResultStatus.Ok,
        RemoveRobotDomainStatus.NotFound => RemoveRobotCommandResultStatus.NotFound,
        RemoveRobotDomainStatus.AggregateNotFound => RemoveRobotCommandResultStatus.Failed,
        RemoveRobotDomainStatus.Failed => RemoveRobotCommandResultStatus.Failed,
        _ => throw new ArgumentOutOfRangeException(nameof(status), $"Not expected direction value: {status}")
    };
}
