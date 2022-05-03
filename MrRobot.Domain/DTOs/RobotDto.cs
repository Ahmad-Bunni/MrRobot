using MrRobot.Domain.Shared;

namespace MrRobot.Domain.DTOs;
public record RobotDto
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public AreaSize AreaSize { get; init; }

    public Coordinates CurrentCoordinates { get; init; }

    public bool IsOutOfBound =>
        CurrentCoordinates.Y > AreaSize.Height ||
        CurrentCoordinates.Y < 0 ||
        CurrentCoordinates.X > AreaSize.Width ||
        CurrentCoordinates.X < 0;

    public RobotDto(Guid id, string name, AreaSize area, Coordinates currentCoordinates)
    {
        Id = id;
        Name = name;
        AreaSize = area;
        CurrentCoordinates = currentCoordinates;
    }
}
