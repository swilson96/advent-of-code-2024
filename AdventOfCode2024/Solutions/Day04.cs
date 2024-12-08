using System.Text.RegularExpressions;
using AdventOfCode2024.Solutions.Cartesian;

namespace AdventOfCode2024.Solutions;

public class Day04 : IAdventSolution
{
    public object PartOne(string input)
    {
        IGrid<char> grid = new Grid<char>(input, x => x);

        return grid.Where(t => t.Item2 == 'X')
            .Select(t => CountInstancesInAllDirections(t.Item1, grid))
            .Sum();
    }

    private int CountInstancesInAllDirections(Point x, IGrid<char> grid)
    {
        if (grid.GetValue(x) != 'X') return 0;

        return x.Surrounds
            .Where(m =>
            {
                var direction = m.Subtract(x);
                var a = m.Add(direction);
                var s = a.Add(direction);

                return grid.InBoundsAndMatches(m, 'M') && grid.InBoundsAndMatches(a, 'A') && grid.InBoundsAndMatches(s, 'S');
            }).Count(); 
    }

    public object PartTwo(string input)
    {
        IGrid<char> grid = new Grid<char>(input, x => x);

        return grid.Where(t => t.Item2 == 'A')
            .Where(t => IsCrossMas(t.Item1, grid))
            .Count();
    }
    
    private bool IsCrossMas(Point x, IGrid<char> grid)
    {
        if (grid.GetValue(x) != 'A') return false;

        if (x.X == 0 || x.X == grid.Bounds.X - 1 || x.Y == 0 || x.Y == grid.Bounds.Y - 1) return false;

        var sw = grid.GetValue(x.X - 1, x.Y + 1);
        var ne = grid.GetValue(x.X + 1, x.Y - 1);
        
        var se = grid.GetValue(x.X + 1, x.Y + 1);
        var nw = grid.GetValue(x.X - 1, x.Y - 1);

        if ((sw != 'M' || ne != 'S') && (sw != 'S' || ne != 'M')) return false;
        if ((nw != 'M' || se != 'S') && (nw != 'S' || se != 'M')) return false;
        
        return true;
    }
}