using Microsoft.EntityFrameworkCore;
using MrRobot.Domain.Entities;
using MrRobot.Domain.Responses;
using MrRobot.Infrastructure;

namespace MrRobot.Core.Services;
internal class RobotsManager : IRobotsManager
{
    private readonly MrRobotDbContext _ctx;

    public RobotsManager(MrRobotDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<CreateRobotDomainResponse> CreateAsync(Robot robot, CancellationToken cancellationToken)
    {
        try
        {
            var aggregateRoot = await _ctx.ControlCenters.FirstOrDefaultAsync(cancellationToken);

            if (aggregateRoot == null) return new CreateRobotDomainResponse { Status = CreateRobotDomainStatus.AggregateNotFound };

            var result = aggregateRoot.CreateRobot(robot);

            if (result != CreateRobotDomainStatus.Ok)
            {
                return new CreateRobotDomainResponse
                {
                    Status = result
                };
            };

            await _ctx.SaveChangesAsync(cancellationToken);

            return new CreateRobotDomainResponse
            {
                Robot = robot,
                Status = CreateRobotDomainStatus.Ok
            };

        }
        catch (Exception)
        {
            // TODO logs

            return new CreateRobotDomainResponse
            {
                Status = CreateRobotDomainStatus.Failed
            };
        }
    }

    public async Task<RemoveRobotDomainStatus> RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var aggregateRoot = await _ctx.ControlCenters.FirstOrDefaultAsync(cancellationToken);

            if (aggregateRoot == null) return RemoveRobotDomainStatus.AggregateNotFound;

            var result = aggregateRoot.RemoveRobot(id);

            if (result == RemoveRobotDomainStatus.Ok)
            {
                await _ctx.SaveChangesAsync(cancellationToken);
            };

            return result;

        }
        catch (Exception)
        {
            // TODO logs

            return RemoveRobotDomainStatus.Failed;
        }
    }

    public async Task<SendCommandsToRobotDomainResponse> SendCommandsToRobotByIdAsync(Guid id, string[] commands, CancellationToken cancellationToken)
    {
        try
        {
            var aggregateRoot = await _ctx.ControlCenters.FirstOrDefaultAsync(cancellationToken);

            if (aggregateRoot == null) return new SendCommandsToRobotDomainResponse { Status = SendCommandsToRobotDomainStatus.AggregateNotFound };

            var result = aggregateRoot.SendCommandToRobotById(id, commands);

            if (result != SendCommandsToRobotDomainStatus.Ok)
            {
                return new SendCommandsToRobotDomainResponse { Status = result };
            };

            await _ctx.SaveChangesAsync(cancellationToken);

            var robot = aggregateRoot.Robots.FirstOrDefault(r => r.Id == id && !r.RemovedDate.HasValue); // TODO use global filters

            return new SendCommandsToRobotDomainResponse
            {
                Robot = robot,
                Status = SendCommandsToRobotDomainStatus.Ok
            };

        }
        catch (Exception)
        {
            // TODO logs

            return new SendCommandsToRobotDomainResponse
            {
                Status = SendCommandsToRobotDomainStatus.Failed
            };
        }
    }

    public async Task<SendCommandsToRobotsDomainResponse> SendCommandsToRobotsAsync(string[] commands, CancellationToken cancellationToken)
    {
        try
        {
            var aggregateRoot = await _ctx.ControlCenters.FirstOrDefaultAsync(cancellationToken);

            if (aggregateRoot == null) return new SendCommandsToRobotsDomainResponse { Status = SendCommandsToRobotsDomainStatus.AggregateNotFound };

            var result = aggregateRoot.SendCommandToRobots(commands);

            if (result != SendCommandsToRobotsDomainStatus.Ok)
            {
                return new SendCommandsToRobotsDomainResponse { Status = result };
            };

            await _ctx.SaveChangesAsync(cancellationToken);

            var robots = aggregateRoot.Robots.Where(r => !r.RemovedDate.HasValue); // TODO use global filters

            return new SendCommandsToRobotsDomainResponse
            {
                Robots = robots,
                Status = SendCommandsToRobotsDomainStatus.Ok
            };

        }
        catch (Exception)
        {
            // TODO logs

            return new SendCommandsToRobotsDomainResponse
            {
                Status = SendCommandsToRobotsDomainStatus.Failed
            };
        }
    }
}
