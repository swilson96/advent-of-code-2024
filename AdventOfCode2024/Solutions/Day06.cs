using AdventOfCode2024.Solutions.Cartesian;

namespace AdventOfCode2024.Solutions;

public class Day06 : IAdventSolution
{
    public object PartOne(string input)
    {
        var grid = Grid<char>.DefaultCharGrid(input);

        var visited = LocationsVisitedByPatrol(grid);
        
        return visited.Count();
    }

    private ISet<Point> LocationsVisitedByPatrol(Grid<char> grid)
    {
        // find the starting point
        var currentState = new MazeState(grid.First(g => g.Item2 == '^').Item1, Direction.D); // Enemy's gate is down
        
        // iterate until we find a loop (somehow hash all states - point + direction?)
        var visited = new HashSet<Point>();
        var historyBag = new HashSet<MazeState>();
        
        while (!historyBag.Contains(currentState) && grid.InBounds(currentState.Position))
        {
            visited.Add(currentState.Position);
            historyBag.Add(currentState);
            
            var target = currentState.Position.Add(currentState.Direction, 1);
            if (grid.InBounds(target) && grid.GetValue(target) == '#')
            {
                // rotate only, don't move until the next iteration
                var orientation = currentState.Direction.RotateAntiClockwise();
                currentState = new MazeState(currentState.Position, orientation);
            }
            else
            {
                currentState = new MazeState(target, currentState.Direction);
            }
        }

        return visited;
    }
    
    private class MazeState
    {
        public readonly Point Position;
        public readonly Direction Direction;

        public MazeState(Point position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }
        
        public override bool Equals(object? obj) => Equals(obj as MazeState);

        private bool Equals(MazeState? other) => other != null && other.Position.Equals(Position) && other.Direction == Direction;

        public override int GetHashCode() => HashCode.Combine(Position, Direction);
    }

    public object PartTwo(string input)
    {
        var grid = Grid<char>.DefaultCharGrid(input);

        var visited = LocationsVisitedByPatrol(grid);
        
        var initialState = new MazeState(grid.First(g => g.Item2 == '^').Item1, Direction.D);

        visited.Remove(initialState.Position);

        var validObstacleLocations = 0;

        foreach (var candidate in visited)
        {
            grid.SetValue(candidate, '#');

            var currentState = initialState;
        
            var historyBag = new HashSet<MazeState>();
        
            while (!historyBag.Contains(currentState) && grid.InBounds(currentState.Position))
            {
                historyBag.Add(currentState);
            
                var target = currentState.Position.Add(currentState.Direction, 1);
                if (grid.InBounds(target) && grid.GetValue(target) == '#')
                {
                    // rotate only, don't move until the next iteration
                    var orientation = currentState.Direction.RotateAntiClockwise();
                    currentState = new MazeState(currentState.Position, orientation);
                }
                else
                {
                    currentState = new MazeState(target, currentState.Direction);
                }
            }

            if (historyBag.Contains(currentState))
            {
                ++validObstacleLocations;
            }
            
            grid.SetValue(candidate, '.');
        }

        return validObstacleLocations;
    }
}