using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solutions;

public class Template : IAdventSolution
{
    private static readonly Regex InputRegex = new (@"Sensor at x=([\d-]+), y=([\d-]+): closest beacon is at x=([\d-]+), y=([\d-]+)");
    
    public object PartOne(string input)
    {
        var lines = input.Split(Environment.NewLine);

        return 0;
    }

    public object PartTwo(string input) => 0;
}