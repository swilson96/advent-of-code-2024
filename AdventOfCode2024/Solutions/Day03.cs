using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solutions;

public class Day03 : IAdventSolution
{
    private readonly Regex _inputRegex = new (@"mul\(([\d-]+),([\d-]+)\)");

    public object PartOne(string input) => ProcessOnSegment(input);

    private int ProcessOnSegment(string input) => _inputRegex.Matches(input)
        .Select(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value))
        .Sum();

    public object PartTwo(string input)
    {
        return input.Split("do()")
            .Select(
                seg => ProcessOnSegment(seg.Split("don't()").First())
                )
            .Sum();
    }
}