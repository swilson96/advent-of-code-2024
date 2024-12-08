using System.Text.RegularExpressions;
using AdventOfCode2024.Solutions.Cartesian;

namespace AdventOfCode2024.Solutions;

public class Day08 : IAdventSolution
{
    public object PartOne(string input)
    {
        var grid = Grid<object>.DefaultCharGrid(input);
        var antinodes = new HashSet<Point>();
        
        foreach (var cell in grid.Where(c => c.Item2 != '.'))
        {
            var frequency = cell.Item2;
            foreach (var pair in grid.Where(c => c.Item2 == frequency && !c.Item1.Equals(cell.Item1)))
            {
                var direction = pair.Item1.Subtract(cell.Item1);
                var antinode1 = pair.Item1.Add(direction);
                var antinode2 = cell.Item1.Subtract(direction);
                if (grid.InBounds(antinode1)) antinodes.Add(antinode1);
                if (grid.InBounds(antinode2)) antinodes.Add(antinode2);
            }
        }

        return antinodes.Count;
    }

    public object PartTwo(string input)
    {
        var grid = Grid<object>.DefaultCharGrid(input);
        var antinodes = new HashSet<Point>();
        
        foreach (var cell in grid.Where(c => c.Item2 != '.'))
        {
            var frequency = cell.Item2;
            foreach (var pair in grid.Where(c => c.Item2 == frequency && !c.Item1.Equals(cell.Item1)))
            {
                var rawDirection = pair.Item1.Subtract(cell.Item1);

                var reducedDirection = Point.SimplifyVector(rawDirection);

                var antinode = cell.Item1;
                while (grid.InBounds(antinode))
                {
                    antinodes.Add(antinode);
                    antinode = antinode.Add(reducedDirection);
                }
                
                antinode = cell.Item1.Subtract(reducedDirection);
                while (grid.InBounds(antinode))
                {
                    antinodes.Add(antinode);
                    antinode = antinode.Subtract(reducedDirection);
                }
            }
        }

        return antinodes.Count;
    }
}