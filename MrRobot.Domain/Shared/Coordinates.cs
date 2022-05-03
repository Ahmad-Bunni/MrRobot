namespace MrRobot.Domain.Shared;

public struct Coordinates
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Coordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Coordinates UpdateCoordinates(Coordinates coordinates)
    {
        X += coordinates.X;
        Y += coordinates.Y;

        return this;
    }
}
