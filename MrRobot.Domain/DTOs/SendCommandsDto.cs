namespace MrRobot.Domain.DTOs;
public record SendCommandsDto
{
    public string[] Commands { get; init; } = null!;
}
