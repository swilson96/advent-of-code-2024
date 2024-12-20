using AdventOfCode2024.Solutions.Cartesian;

namespace AdventOfCode2024.Solutions;

public class Day20 : IAdventSolution
{
    public object PartOne(string input) => HowManyCheatsSaveAtLeast(input, 100);

    public int HowManyCheatsSaveAtLeast(string input, int savingsCutoff)
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

            if (unvisited.Count > 0)
            {
                current = unvisited.OrderBy(u => distance[u.Y][u.X]).First();
            }
        }
        
        var cheatsSavingAtLeastCutoff = 0;
        foreach (var wall in grid.Where(t => t.Item2 == '#'))
        {
            var entryPoints = wall.Item1.Neighbours
                .Where(n => grid.InBounds(n) && grid.GetValue(n) != '#')
                .ToList();

            if (!entryPoints.Any())
            {
                continue;
            }

            var distanceToWall = entryPoints.Select(e => distance[e.Y][e.X]).Min() + 1;
            
            var cheats = wall.Item1.Neighbours
                .Where(n => grid.InBounds(n) && grid.GetValue(n) != '#')
                .ToList();

            foreach (var cheat in cheats)
            {
                var distanceWithCheat = new int[grid.Bounds.Y][];

                for (var y = 0; y < grid.Bounds.Y; ++y)
                {
                    distanceWithCheat[y] = new int[grid.Bounds.X];
                    for (var x = 0; x < grid.Bounds.X; ++x)
                    {
                        distanceWithCheat[y][x] = distance[y][x];
                    }
                }

                var toVisitNext = new HashSet<Point> { cheat };
                distanceWithCheat[wall.Item1.Y][wall.Item1.X] = distanceToWall;
                distanceWithCheat[cheat.Y][cheat.X] = distanceToWall + 1;

                while (toVisitNext.Count > 0)
                {
                    var currentWithCheat = toVisitNext.OrderBy(u => distance[u.Y][u.X]).First();
                    foreach (var neighbour in currentWithCheat.Neighbours)
                    {
                        if (!grid.InBounds(neighbour) || grid.GetValue(neighbour) == '#')
                        {
                            continue;
                        }

                        var newDistance = distanceWithCheat[currentWithCheat.Y][currentWithCheat.X] + 1;
                        if (newDistance < distanceWithCheat[neighbour.Y][neighbour.X])
                        {
                            distanceWithCheat[neighbour.Y][neighbour.X] = newDistance;
                            toVisitNext.Add(neighbour);
                        }
                    }

                    toVisitNext.Remove(currentWithCheat);
                }

                if (distance[destination.Y][destination.X] - distanceWithCheat[destination.Y][destination.X] >=
                    savingsCutoff)
                {
                    ++cheatsSavingAtLeastCutoff;
                }
            }
        }

        return cheatsSavingAtLeastCutoff;
    }

    public int ShortestDistance(string input)
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