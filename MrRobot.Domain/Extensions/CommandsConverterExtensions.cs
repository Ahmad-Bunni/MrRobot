using MrRobot.Domain.Shared;

namespace MrRobot.Domain.Extensions;
public static class CommandsConverterExtensions
{
    public static IEnumerable<Coordinates> CommandsToCoordinates(this string[] commands)
    {
        foreach (var command in commands)
        {
            yield return ToCoordinates(command);
        }
    }

    private static Coordinates ToCoordinates(string command) => command.ToLower() switch
    {
        "advance" => new Coordinates(0, 1),
        "retreat" => new Coordinates(0, -1),
        "right" => new Coordinates(1, 0),
        "left" => new Coordinates(-1, 0),
        _ => throw new ArgumentOutOfRangeException(nameof(command), $"Not expected direction value: {command}")
    };
}