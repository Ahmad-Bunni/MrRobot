using MediatR;
using MrRobot.Core.Services;
using MrRobot.Domain.Contracts.Queries.Robots;
using MrRobot.Domain.DTOs;

namespace MrRobot.Core.Handlers.Commands.Robots;

public class GetRobotsQueryHandler : IRequestHandler<GetRobotsQuery, IEnumerable<RobotDto>>
{
    private readonly IRobotsProvider _robotsProvider;

    public GetRobotsQueryHandler(IRobotsProvider robotsProvider)
    {
        _robotsProvider = robotsProvider;
    }

    public async Task<IEnumerable<RobotDto>> Handle(GetRobotsQuery request, CancellationToken cancellationToken)
    {
        var result = await _robotsProvider.GetRobotsAsync(cancellationToken);

        return result.Select(r => r.ToDto());
    }
}
