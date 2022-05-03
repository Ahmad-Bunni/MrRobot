using MrRobot.Domain.Extensions;
using MrRobot.Domain.Responses;

namespace MrRobot.Domain.Entities;

public interface IAggregateRoot { }

public class ControlCenter : BaseEntity, IAggregateRoot
{

    private readonly HashSet<Robot> _robots = new();
    public IReadOnlyCollection<Robot> Robots => _robots;

    public ControlCenter()
    {
        _robots = new HashSet<Robot>();
    }

    public CreateRobotDomainStatus CreateRobot(Robot robot)
    {
        if (_robots.Any(r => r.Name.Equals(robot.Name, StringComparison.OrdinalIgnoreCase) && !r.RemovedDate.HasValue))
        {
            return CreateRobotDomainStatus.AlreadyExists;
        }

        _robots.Add(robot);

        return CreateRobotDomainStatus.Ok;
    }

    public RemoveRobotDomainStatus RemoveRobot(Guid id)
    {
        var robot = _robots.FirstOrDefault(r => r.Id == id && !r.RemovedDate.HasValue);

        if (robot == null)
        {
            return RemoveRobotDomainStatus.NotFound;
        }

        robot.MarkAsRemoved();

        return RemoveRobotDomainStatus.Ok;
    }

    public SendCommandsToRobotDomainStatus SendCommandToRobotById(Guid id, string[] commands)
    {
        var robot = _robots.FirstOrDefault(r => r.Id == id && !r.RemovedDate.HasValue);

        if (robot == null)
        {
            return SendCommandsToRobotDomainStatus.NotFound;
        }

        robot.ProcessCoordinates(commands.CommandsToCoordinates());

        return SendCommandsToRobotDomainStatus.Ok;
    }

    public SendCommandsToRobotsDomainStatus SendCommandToRobots(string[] commands)
    {

        if (!_robots.Any(r => !r.RemovedDate.HasValue))
        {
            return SendCommandsToRobotsDomainStatus.NotFound;
        }

        foreach (var robot in _robots)
        {
            robot.ProcessCoordinates(commands.CommandsToCoordinates());
        }

        return SendCommandsToRobotsDomainStatus.Ok;
    }
}
