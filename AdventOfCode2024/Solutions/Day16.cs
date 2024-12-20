using System.Text.RegularExpressions;
using AdventOfCode2024.Solutions.Cartesian;

namespace AdventOfCode2024.Solutions;

public class Day16 : IAdventSolution
{
    private static readonly Regex InputRegex = new (@"Sensor at x=([\d-]+), y=([\d-]+): closest beacon is at x=([\d-]+), y=([\d-]+)");
    
    public object PartOne(string input)
    {
        var grid = Grid<char>.DefaultCharGrid(input);
        
        var unvisited = new HashSet<Point>();
        var distance = new int[grid.Bounds.Y][];
        var current = Point.Origin;
        var destination = Point.Origin;
        for (var y = 0; y < grid.Bounds.Y; ++y)
        {
            distance[y] = new int[grid.Bounds.X];
            for (var x = 0; x < grid.Bounds.X; ++x)
            {
                if (grid.GetValue(x, y) == '.')
                {
                    unvisited.Add(new Point(x, y));
                    distance[y][x] = int.MaxValue;
                }
                else if (grid.GetValue(x, y) == 'S')
                {
                    current = new Point(x, y);
                    distance[y][x] = 0;
                }
                else if (grid.GetValue(x, y) == 'E')
                {
                    destination = new Point(x, y);
                    unvisited.Add(destination);
                    distance[y][x] = int.MaxValue;
                }
            }
        }

        while (unvisited.Count > 0)
        {
            var distanceToCurrent = distance[current.Y][current.X];
            foreach (var neighbour in current.Neighbours)
            {
                // if (grid.GetValue(neighbour) == '#')
                // {
                //     continue;
                // }

                if (!unvisited.Contains(neighbour))
                {
                    continue;
                }

                distance[neighbour.Y][neighbour.X] = Math.Min(distance[neighbour.Y][neighbour.X], distance[current.Y][current.X] + 1);
            }
            
            unvisited.Remove(current);
            
            if (current.Equals(destination))
            {
                return distance[destination.Y][destination.X];
            }

            current = unvisited.OrderBy(u => distance[u.Y][u.X]).First();
        }

        return -1;
    }

    public object PartTwo(string input) => 0;
}