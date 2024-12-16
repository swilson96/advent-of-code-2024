namespace AdventOfCode2024.Solutions;

public class Day05 : IAdventSolution
{
    public object PartOne(string input)
    {
        var parts = input.Split(Environment.NewLine + Environment.NewLine).ToArray();
        var rules = parts[0].Split(Environment.NewLine).Select(l => l.Split("|").Select(int.Parse).ToArray()).ToList();
        
        var updates = parts[1].Split(Environment.NewLine).Select(l => l.Split(",").Select(int.Parse).ToList());

        return updates
                .Where(u => rules.All(r => u.IndexOf(r[0]) < u.IndexOf(r[1]) || u.IndexOf(r[1]) < 0))
                .Select(u => u[u.Count / 2])
                .Sum();
    }

    public object PartTwo(string input) {
        var parts = input.Split(Environment.NewLine + Environment.NewLine).ToArray();
        var rules = parts[0].Split(Environment.NewLine).Select(l => l.Split("|").Select(int.Parse).ToArray()).ToList();
        
        var updates = parts[1]
            .Split(Environment.NewLine)
            .Select(l => l.Split(",").Select(int.Parse).ToList())
            .Where(u => !rules.All(r => u.IndexOf(r[0]) < u.IndexOf(r[1]) || u.IndexOf(r[1]) < 0))
            .ToList();

        foreach (var update in updates)
        {
            int i, j, temp;
            bool swapped;
            for (i = 0; i < update.Count - 1; i++) {
                swapped = false;
                for (j = 0; j < update.Count - i - 1; j++)
                {
                    var rule = rules.FirstOrDefault(r => r[0] == update[j+1] && r[1] == update[j]);
                    if (rule != null){
                        // Swap arr[j] and arr[j+1]
                        temp = update[j];
                        update[j] = update[j + 1];
                        update[j + 1] = temp;
                        swapped = true;
                    }
                }

                // If no two elements were
                // swapped by inner loop, then break
                if (swapped == false)
                    break;
            }
        }
        
        return updates
            .Select(u => u[u.Count / 2])
            .Sum();
    }
}