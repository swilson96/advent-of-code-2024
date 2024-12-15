using System.Numerics;

namespace AdventOfCode2024.Solutions.Cartesian;

public class Point
{
    public static Point Origin => new(0, 0);
    
    public int X { get; }
    public int Y { get; }

    /// <summary>
    /// The four neighbours in each Direction
    /// </summary>
    public IEnumerable<Point> Neighbours => Enum.GetValues<Direction>().Select(d => this.Add(d, 1));
    
    /// <summary>
    /// The eight cells that completely surround this one. Neighbours combined with "diagonal neighbours".
    /// </summary>
    public IEnumerable<Point> Surrounds {
        get
        {
            foreach (var n in Neighbours)
            {
                yield return n;
            }
            yield return new Point(X + 1, Y + 1);
            yield return new Point(X + 1, Y - 1);
            yield return new Point(X - 1, Y + 1);
            yield return new Point(X - 1, Y - 1);
        }
    }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point Add(Direction direction, int distance) => direction switch
    {
        Direction.U => new Point(X, Y + distance),
        Direction.R => new Point(X + distance, Y),
        Direction.D => new Point(X, Y - distance),
        Direction.L => new Point(X - distance, Y),
        _ => throw new ArgumentOutOfRangeException($"Direction {direction} unexpected")
    };

    public Point Add(Point other) => Add(other, 1);

    public Point Subtract(Point other) => Add(other, -1);

    public Point Add(Point other, int times) => new(X + other.X * times, Y + other.Y * times);

    public override bool Equals(object? obj) => Equals(obj as Point);

    public bool Equals(Point? other) => other != null && other.X == X && other.Y == Y;

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public override string ToString() => $"Point({X}, {Y})";

    public static int ManhattanDistance(Point a, Point b) => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);

    public static Point SimplifyVector(Point vector)
    {
        var x = vector.X;
        var y = vector.Y;
        
        if (vector.X == 0 || vector.Y == 0) return vector;
        if (vector.X % vector.Y == 0) return new Point(vector.X / vector.Y, 1);
        if (vector.Y % vector.X == 0) return new Point(1, vector.Y / vector.X);
            
        var limit = Math.Min(Math.Abs(x), Math.Abs(y)) / 2;
        for (int p = 2; p <= limit; ++p)
        {
            while (x % p == 0 && y % p == 0)
            {
                x /= p;
                y /= p;
            }
        }

        return new Point(x, y);
    }
}