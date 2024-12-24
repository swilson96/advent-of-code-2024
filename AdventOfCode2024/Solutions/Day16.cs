using AdventOfCode2024.Solutions.Cartesian;

namespace AdventOfCode2024.Solutions;

public class Day16 : IAdventSolution
{
    public object PartOne(string input)
    {
        var grid = Grid<char>.DefaultCharGrid(input);

        // var (distance, _, end) = PopulateDistancesToEnd(grid);

        // return distance[end];

        return 0;
    }

    private Tuple<Dictionary<Loc, int>, Loc, Loc> PopulateDistancesToEnd(Grid<char> grid)
    {
        var unvisited = new HashSet<Loc>();
        var start = new Loc(Point.Origin, Direction.R);
        var current = start;
        var destination = Point.Origin;
        
        var distance = new Dictionary<Loc, int>();
        
        foreach (var cell in grid)
        {
            if (cell.Item2 == '.' || cell.Item2 == 'E')
            {
                unvisited.Add(new Loc(cell.Item1, Direction.D));
                unvisited.Add(new Loc(cell.Item1, Direction.R));
                unvisited.Add(new Loc(cell.Item1, Direction.U));
                unvisited.Add(new Loc(cell.Item1, Direction.L));
                distance.Add(new Loc(cell.Item1, Direction.D), int.MaxValue);
                distance.Add(new Loc(cell.Item1, Direction.R), int.MaxValue);
                distance.Add(new Loc(cell.Item1, Direction.U), int.MaxValue);
                distance.Add(new Loc(cell.Item1, Direction.L), int.MaxValue);
                    
                if (cell.Item2 == 'E')
                {
                    destination = cell.Item1;
                }
            }
            else if (cell.Item2 == 'S')
            {
                current = new Loc(cell.Item1, Direction.R);
                unvisited.Add(new Loc(cell.Item1, Direction.D));
                unvisited.Add(new Loc(cell.Item1, Direction.U));
                unvisited.Add(new Loc(cell.Item1, Direction.L));
                distance.Add(new Loc(cell.Item1, Direction.D), int.MaxValue);
                distance.Add(new Loc(cell.Item1, Direction.R), 0);
                distance.Add(new Loc(cell.Item1, Direction.U), int.MaxValue);
                distance.Add(new Loc(cell.Item1, Direction.L), int.MaxValue);
            }
        }

        while (unvisited.Count > 0)
        {
            if (current.P.Equals(destination))
            {
                return new Tuple<Dictionary<Loc, int>, Loc, Loc>(distance, start, current);
            }
            
            var distanceToCurrent = distance[current]; // for debug

            foreach (var move in current.GetNextMoves())
            {
                if (!unvisited.Contains(move.Location))
                {
                    continue;
                }

                distance[move.Location] = Math.Min(distance[move.Location], distance[current] + move.Cost);
            }
            
            unvisited.Remove(current);

            current = unvisited.OrderBy(u => distance[u]).First();
        }

        throw new Exception("didn't find the end!");
    }

    public object PartTwo(string input)
    {
        var grid = Grid<char>.DefaultCharGrid(input);

        var (distance, _, end) = PopulateDistancesToEnd(grid);

        var tilesOnRoutes = new HashSet<Point> { end.P };
        
        var possiblePrecursors = new Queue<Loc>();
        foreach (var precursor in new List<Loc> {
                new(end.P.Add(Direction.U, 1), Direction.D),
                new(end.P.Add(Direction.D, 1), Direction.U),
                new(end.P.Add(Direction.L, 1), Direction.R),
                new(end.P.Add(Direction.R, 1), Direction.L)
            }.Where(l => grid.InBounds(l.P) && grid.GetValue(l.P) == '.')) {
            if (distance[precursor] == distance[end] - 1)
            {
                possiblePrecursors.Enqueue(precursor);
                tilesOnRoutes.Add(precursor.P);
            }
        }

        while (possiblePrecursors.Any())
        {
            var current = possiblePrecursors.Dequeue();

            var moves = current.GetPreviousMoves();

            foreach (var move in moves)
            {
                if (grid.InBounds(move.Location.P) && grid.GetValue(move.Location.P) != '#' && distance[move.Location] == distance[current] - move.Cost)
                {
                    possiblePrecursors.Enqueue(move.Location);
                    tilesOnRoutes.Add(move.Location.P);
                }
            }
        }

        return tilesOnRoutes.Count;
    }

    private class Loc
    {
        public readonly Point P;
        public readonly Direction D;

        public Loc(Point p, Direction d)
        {
            P = p;
            D = d;
        }

        public IEnumerable<Move> GetNextMoves()
        {
            yield return new Move(P.Add(D, 1), D, 1);
            yield return new Move(P, D.RotateAntiClockwise(), 1000);
            yield return new Move(P, D.RotateAntiClockwise().RotateAntiClockwise().RotateAntiClockwise(), 1000);
        }
        
        public IEnumerable<Move> GetPreviousMoves()
        {
            yield return new Move(P.Add(D, -1), D, 1);
            yield return new Move(P, D.RotateAntiClockwise(), 1000);
            yield return new Move(P, D.RotateAntiClockwise().RotateAntiClockwise().RotateAntiClockwise(), 1000);
        }

        public override bool Equals(object? obj) => Equals(obj as Loc);

        // Not null-safe yet
        public bool Equals(Loc? other) => other != null && other.P.Equals(P) && other.D.Equals(D);

        public override int GetHashCode() => HashCode.Combine(P, D);

        public override string ToString() => $"Loc({P}, {D})";
        
        public class Move
        {
            public readonly Loc Location;
            public readonly int Cost;

            public Move(Point p, Direction d, int c)
            {
                Location = new Loc(p, d);
                Cost = c;
            }
        }
    }
}