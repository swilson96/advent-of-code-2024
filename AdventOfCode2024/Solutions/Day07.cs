using System.Collections;

namespace AdventOfCode2024.Solutions;

public class Day07 : IAdventSolution
{
    public object PartOne(string input) => input
        .Split(Environment.NewLine)
        .Where(CanBeValid)
        .Select(l => l.Split(": ")[0])
        .Select(long.Parse)
        .Sum();

    public bool CanBeValid(string line)
    {
        var parts = line.Split(": ");
        var target = long.Parse(parts[0]);
        var terms = parts[1].Split(" ").Select(int.Parse).ToList();

        var gaps = terms.Count - 1;
        for (var flags = 0; flags < 1 << gaps; ++flags)
        {
            var result = terms
                .Select((t, i) => new Tuple<int, int>(t, ReadBit(flags, i-1)))
                .Aggregate(0L, (a, b) => b.Item2 == 0 ? a + b.Item1 : a * b.Item1);
            if (result == target)
            {
                return true;
            }
        }

        return false;
    }
    
    private int ReadBit(long n, int k)
    {
        if (k < 0) return 0; // specific to this algorithm
        var temp = n >> k;
        return (int) temp & 1;
    }

    public object PartTwo(string input) => 0;
}