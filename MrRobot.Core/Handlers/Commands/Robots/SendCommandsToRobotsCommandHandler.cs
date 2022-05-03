using MediatR;
using MrRobot.Core.Services;
using MrRobot.Domain.Contracts.Commands.Robots;
using MrRobot.Domain.Responses;

namespace MrRobot.Core.Handlers.Commands.Robots;

public class SendCommandsToRobotsCommandHandler : IRequestHandler<SendCommandsToRobotsCommand, SendCommandsToRobotsCommandResult>
{
    private readonly IRobotsManager _robotsManager;

    public SendCommandsToRobotsCommandHandler(IRobotsManager robotsManager)
    {
        _robotsManager = robotsManager;
    }

    public async Task<SendCommandsToRobotsCommandResult> Handle(SendCommandsToRobotsCommand request, CancellationToken cancellationToken)
    {
        var result = await _robotsManager.SendCommandsToRobotsAsync(request.Payload.Commands, cancellationToken);

        if (result.Status == SendCommandsToRobotsDomainStatus.Ok)
        {
            return new SendCommandsToRobotsCommandResult
            {
                Payload = result.Robots.Select(robot => robot.ToDto()),
                Status = MapResult(result.Status),
            };
        }

        return new SendCommandsToRobotsCommandResult
        {
            Status = MapResult(result.Status)
        };

    }

    private static SendCommandsToRobotsCommandResultStatus MapResult(SendCommandsToRobotsDomainStatus status) => status switch
    {
        SendCommandsToRobotsDomainStatus.Ok => SendCommandsToRobotsCommandResultStatus.Ok,
        SendCommandsToRobotsDomainStatus.NotFound => SendCommandsToRobotsCommandResultStatus.NotFound,
        SendCommandsToRobotsDomainStatus.AggregateNotFound => SendCommandsToRobotsCommandResultStatus.Failed,
        SendCommandsToRobotsDomainStatus.Failed => SendCommandsToRobotsCommandResultStatus.Failed,
        _ => throw new ArgumentOutOfRangeException(nameof(status), $"Not expected direction value: {status}")
    };
}
