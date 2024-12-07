namespace AdventOfCode2024.Solutions;

public class Day01 : IAdventSolution
{
    public object PartOne(string input)
    {
        var unorderedPairs = input.Split(Environment.NewLine)
            .Select(l => l.Split("   "))
            .Select(a => a.Select(int.Parse).ToArray())
            .ToList();
        
        var left = unorderedPairs.Select(p => p[0]).ToList();
        left.Sort();
        
        var right = unorderedPairs.Select(p => p[1]).ToList();
        right.Sort();

        return left.Zip(right).Select(a => Math.Abs(a.First - a.Second)).Sum();
    }

    public object PartTwo(string input)
    {
        var unorderedPairs = input.Split(Environment.NewLine)
            .Select(l => l.Split("   "))
            .Select(a => a.Select(int.Parse).ToArray())
            .ToList();
        
        var left = unorderedPairs.Select(p => p[0]).ToList();
        
        var rightCounts = unorderedPairs.Select(p => p[1])
            .GroupBy(a => a)
            .ToDictionary(g => g.Key, g => g.Count());

        return left.Select(a => rightCounts.ContainsKey(a) ? a * rightCounts[a] : 0).Sum();
    }
}