using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solutions;

public class Day02 : IAdventSolution
{
    public object PartOne(string input)
    {
        return input.Split(Environment.NewLine)
            .Where(ReportIsSafe)
            .Count();
    }

    private static bool ReportIsSafe(string report)
    {
        var levels = report.Split(" ").Select(int.Parse).ToList();
        return ReportIsSafe(levels);
    }
    
    private static bool ReportIsSafe(List<int> levels )
    {
        var increasing = levels[1] > levels[0];
        for (var i = 1; i < levels.Count; ++i)
        {
            if (increasing)
            {
                if (levels[i] <= levels[i - 1]) return false;
                if (levels[i - 1] + 3 < levels[i]) return false;
            }
            else
            {
                if (levels[i] >= levels[i - 1]) return false;
                if (levels[i] + 3 < levels[i - 1]) return false;
            }
        }

        return true;
    }

    public object PartTwo(string input) => input.Split(Environment.NewLine)
        .Where(ReportIsSafeWthDampener)
        .Count();
    
    private static bool ReportIsSafeWthDampener(string report)
    {
        var levels = report.Split(" ").Select(int.Parse).ToList();
        var increasing = levels[1] > levels[0];
        var firstProblemIndex = false;
        for (var i = 1; i < levels.Count; ++i)
        {
            if (increasing)
            {
                if (levels[i] <= levels[i - 1]) firstProblemIndex = true;
                if (levels[i - 1] + 3 < levels[i]) firstProblemIndex = true;
            }
            else
            {
                if (levels[i] >= levels[i - 1]) firstProblemIndex = true;
                if (levels[i] + 3 < levels[i - 1]) firstProblemIndex = true;
            }
            
            if (firstProblemIndex)
            {
                if (ReportIsSafe(
                        levels.Take(i - 1).ToList().Concat(levels.Skip(i))
                            .ToList())) return true;
                if (ReportIsSafe(levels.Take(i).Concat(levels.Skip(i + 1)).ToList())) return true;
                if (i == 2 && ReportIsSafe(levels.Skip(1).ToList())) return true; 
                return false;
            }
        }
        return true;
    }
}