using System.Collections;

namespace AdventOfCode2024.Solutions.Cartesian;

public interface IGrid<T> : IEnumerable<Tuple<Point, T>>
{
    T GetValue(int x, int y);
    
    Point Bounds { get; }

    public T GetValue(Point p) => GetValue(p.X, p.Y);
    
    bool InBoundsAndMatches(Point p, T valueToMatch);
}

public class Grid<T> : IGrid<T>
{
    private readonly T[][] _grid;
    
    public Point Bounds { get; }

    public Grid(string input, Func<char, T> parse)
    {
        _grid = input.Split(Environment.NewLine).Select(l => l.ToCharArray().Select(parse).ToArray()).ToArray();

        Bounds = new Point(_grid[0].Length, _grid.Length);
    }

    public T GetValue(int x, int y)
    {
        return _grid[y][x];
    }
    
    public bool InBoundsAndMatches(Point p, T valueToMatch)
    {
        return p.X >= 0 && p.X < Bounds.X && p.Y >= 0 && p.Y < Bounds.Y && GetValue(p.X, p.Y).Equals(valueToMatch);
    }

    public IEnumerator<Tuple<Point, T>> GetEnumerator()
    {
        for (var y = 0; y < Bounds.Y; ++y)
        {
            for (var x = 0; x < Bounds.X; ++x)
            {
                yield return new Tuple<Point, T>(new Point(x, y), GetValue(x, y));
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}