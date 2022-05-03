using Microsoft.EntityFrameworkCore;
using MrRobot.Domain.Entities;
using MrRobot.Infrastructure;

namespace MrRobot.Core.Services;
internal class RobotsProvider : IRobotsProvider
{
    // we could use an ORM like dapper instead for reading
    private readonly MrRobotDbContext _ctx;

    public RobotsProvider(MrRobotDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<Robot>> GetRobotsAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _ctx.Robots.Where(r => !r.RemovedDate.HasValue).ToListAsync(cancellationToken); // TODO global filter for removed date
        }
        catch (Exception)
        {
            // TODO logs

            return Array.Empty<Robot>();
        }
    }
}
