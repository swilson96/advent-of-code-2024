using System.Collections;

namespace AdventOfCode2024.Solutions;

public class Day07 : IAdventSolution
{
    public object PartOne(string input) => SumValidAccordingTo(input, 2);
    
    public object PartTwo(string input) => SumValidAccordingTo(input, 3);
    
    public long SumValidAccordingTo(string input, int numOps) => input
        .Split(Environment.NewLine)
        .Where(l => CanBeValid(l, numOps))
        .Select(l => l.Split(": ")[0])
        .Select(long.Parse)
        .Sum();

    public bool CanBeValid(string line, int numOps)
    {
        var parts = line.Split(": ");
        var target = long.Parse(parts[0]);
        var terms = parts[1].Split(" ").Select(int.Parse).ToList();

        var gaps = terms.Count - 1;
        for (var flags = 0; flags < Math.Pow(numOps, gaps); ++flags)
        {
            var result = terms
                .Select((t, i) => new Tuple<int, int>(t, ReadBit(flags, i-1, numOps)))
                .Aggregate(0L, (a, b) => b.Item2 == 0 
                    ? a + b.Item1 
                    : b.Item2 == 1 
                        ? a * b.Item1
                        : long.Parse(a.ToString() + b.Item1.ToString()));
            if (result == target)
            {
                return true;
            }
        }

        return false;
    }
    
    private int ReadBit(long n, int k, int numOps)
    {
        if (k < 0) return 0; // specific to this algorithm
        var temp = n / Math.Pow(numOps, k);
        var ret = (int) temp % numOps;
        if (ret < 0)
        {
            ret += numOps;
        }

        return ret;
    }
}