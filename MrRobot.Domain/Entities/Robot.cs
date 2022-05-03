using MrRobot.Domain.DTOs;
using MrRobot.Domain.Shared;

namespace MrRobot.Domain.Entities;
public class Robot : BaseEntity
{
    public string Name { get; private set; } = null!;
    public AreaSize AreaSize { get; private set; } = null!;
    public Coordinates CurrentCoordinates { get; private set; }
    public DateTimeOffset DeployedDate { get; private set; }
    public DateTimeOffset? RemovedDate { get; private set; }
    public ControlCenter ControlCenter { get; private set; } = null!;

    private Robot() { } // required by ef

    public Robot(string name, AreaSize areaSize)
    {
        Id = Guid.NewGuid();
        Name = name;
        AreaSize = areaSize;
        CurrentCoordinates = new Coordinates(0, 0);
        DeployedDate = DateTimeOffset.Now;
    }

    public void MarkAsRemoved() => RemovedDate = DateTimeOffset.Now;

    public void ProcessCoordinates(IEnumerable<Coordinates> coordinates)
    {
        foreach (var points in coordinates)
        {
            CurrentCoordinates = CurrentCoordinates.UpdateCoordinates(points);
        }
    }

    public RobotDto ToDto() => new(Id, Name, AreaSize, CurrentCoordinates);
}
