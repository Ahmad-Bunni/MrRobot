using MrRobot.Domain.Entities;
using MrRobot.Domain.Shared;

namespace MrRobot.Domain.DTOs;

public record CreateRobotDto
{
    public string Name { get; init; }
    public AreaSize AreaSize { get; init; }

    public CreateRobotDto(string name, AreaSize areaSize)
    {
        Name = name;

        AreaSize = areaSize;
    }

    public Robot ToModel() => new(Name, AreaSize);
}
