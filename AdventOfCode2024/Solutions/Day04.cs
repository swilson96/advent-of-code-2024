using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solutions;

public class Day04 : IAdventSolution
{
    private readonly Regex _inputRegex = new (@"Sensor at x=([\d-]+), y=([\d-]+): closest beacon is at x=([\d-]+), y=([\d-]+)");
    
    public object PartOne(string input)
    {
        var lines = input.Split(Environment.NewLine);

        return 0;
    }

    public object PartTwo(string input) => 0;
}