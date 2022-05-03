using MediatR;
using MrRobot.Core.Services;
using MrRobot.Domain.Contracts.Commands.Robots;
using MrRobot.Domain.Responses;

namespace MrRobot.Core.Handlers.Commands.Robots;

public class SendCommandsToRobotCommandHandler : IRequestHandler<SendCommandsToRobotCommand, SendCommandsToRobotCommandResult>
{
    private readonly IRobotsManager _robotsManager;

    public SendCommandsToRobotCommandHandler(IRobotsManager robotsManager)
    {
        _robotsManager = robotsManager;
    }

    public async Task<SendCommandsToRobotCommandResult> Handle(SendCommandsToRobotCommand request, CancellationToken cancellationToken)
    {
        var result = await _robotsManager.SendCommandsToRobotByIdAsync(request.Id, request.Payload.Commands, cancellationToken);

        return new SendCommandsToRobotCommandResult
        {
            Payload = result.Robot?.ToDto(),
            Status = MapResult(result.Status),
        };
    }

    private static SendCommandsToRobotCommandResultStatus MapResult(SendCommandsToRobotDomainStatus status) => status switch
    {
        SendCommandsToRobotDomainStatus.Ok => SendCommandsToRobotCommandResultStatus.Ok,
        SendCommandsToRobotDomainStatus.NotFound => SendCommandsToRobotCommandResultStatus.NotFound,
        SendCommandsToRobotDomainStatus.AggregateNotFound => SendCommandsToRobotCommandResultStatus.Failed,
        SendCommandsToRobotDomainStatus.Failed => SendCommandsToRobotCommandResultStatus.Failed,
        _ => throw new ArgumentOutOfRangeException(nameof(status), $"Not expected direction value: {status}")
    };
}
