using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode2024.Solutions.Cartesian;

namespace AdventOfCode2024.Solutions;

public class Day14 : IAdventSolution
{
    private static readonly Regex InputRegex = new (@"p=([\d-]+),([\d-]+) v=([\d-]+),([\d-]+)");

    private const int Seconds = 100;
    
    private int Mod(int x, int m) {
        var r = x % m;
        return r < 0 ? r + m : r;
    }

    public object PartOne(string input) => PartOne(input, 101, 103); 
    
    public int PartOne(string input, int w, int h) => input.Split(Environment.NewLine)
            .Select(Robot.Build)
            .Select(r => new Robot(r.Position.Add(r.Velocity, Seconds), r.Velocity))
            .Select(r => new Robot(new Point(Mod(r.Position.X, w), Mod(r.Position.Y, h)), r.Velocity))
            .Where(r => r.Quadrant(w, h) >= 0)
            .GroupBy(r => r.Quadrant(w, h))
            .Select(g => g.Count())
            .Aggregate(1, (curr, acc) => curr * acc);

    private List<Robot> IterateRobots(IEnumerable<Robot> robots, int times, int w, int h) => robots
        .Select(r => new Robot(r.Position.Add(r.Velocity, times), r.Velocity))
        .Select(r => new Robot(new Point(Mod(r.Position.X, w), Mod(r.Position.Y, h)), r.Velocity))
        .ToList();

    public object PartTwo(string input)
    {
        const int w = 101;
        const int h = 103;
        
        var robots = input.Split(Environment.NewLine)
            .Select(Robot.Build)
            .ToList();
        robots = IterateRobots(robots, 90, w, h);
        
        var time = 90;
        while (time < w * h)
        {
            // Test for increasing quantities as you go up
            // var yfreqs = Enumerable.Range(0, h)
            //     .Select(y => robots.Count(r => r.Position.Y == y))
            //     .ToList();
            // var yJumpDowns = yfreqs.Skip(1).Zip(yfreqs)
            //     .Select(ys => ys.First - ys.Second)
            //     .Count(j => j > 0); // First is higher index, second is lower 
            
            // test for solid trunk
            var trunkLength = robots
                .Where(r => r.Position.X == w / 2)
                .Select(r => r.Position.Y)
                .Distinct()
                .Count();
            
            // Test for pairs on rows
            // var counts = Enumerable.Range(0, h)
            //     .Select(y => robots.Count(r => r.Position.Y == y)).ToList();
            // var quadsOrLess = counts.Count(c => c <= 4);
            // var emptyRows = counts.Count(c => c == 0);

            if (time == 6888)
            {
                Console.WriteLine("");
                Console.WriteLine("time={0}", time);
                PrintRobots(robots, w, h);
            }
            
            // iterate once
            // robots = robots.Select(r => new Robot(new Point(Mod(r.Position.X + r.Velocity.X, w), Mod(r.Position.Y + r.Velocity.Y, h)), r.Velocity))
            //     .ToList();

            robots = IterateRobots(robots, 103, w, h);

            time += 103;
        }

        return 0;
    }

    private void PrintRobots(IList<Robot> robots, int w, int h)
    {
        for (var y = 10; y < 60; ++y)
        {
            var b = new StringBuilder();
            for (var x = 0; x < w; ++x)
            {
                b.Append(robots.Any(r => r.Position.Equals(new Point(x, y))) ? 'X' : '.');
            }

            Console.WriteLine(b.ToString());
        }
    }

    private class Robot
    {
        public readonly Point Position;
        public readonly Point Velocity;

        public Robot(Point p, Point v)
        {
            Position = p;
            Velocity = v;
        }
        
        public static Robot Build(string input)
        {
            var match = InputRegex.Match(input);
            var p = new Point(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            var v = new Point(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
            return new Robot(p, v);
        }

        public int Quadrant(int w, int h)
        {
            if (Position.X < w / 2 && Position.Y < h / 2) return 0;
            if (Position.X > w / 2 && Position.Y < h / 2) return 1;
            if (Position.X > w / 2 && Position.Y > h / 2) return 2;
            if (Position.X < w / 2 && Position.Y > h / 2) return 3;
            return -1;
        }
    }
}