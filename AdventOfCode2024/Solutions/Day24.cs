using System.Text;
using System.Text.RegularExpressions;
using MoreLinq;

namespace AdventOfCode2024.Solutions;

public class Day24 : IAdventSolution
{
    private static readonly Regex InputRegex = new (@"([\w]{3}) ([\w]+) ([\w]{3}) -> ([\w]{3})");
    
    public object PartOne(string input)
    {
        var wires = new Dictionary<string, int>();
        var rules = ParseInput(input, wires);

        var outputBitKeys = rules
            .Select(r => r.Groups[4].Value)
            .Where(v => v.StartsWith('z'))
            .OrderBy(v => v)
            .ToList();

        while (!outputBitKeys.All(wires.ContainsKey))
        {
            foreach (var rule in rules)
            {
                if (wires.ContainsKey(rule.Groups[4].Value) || !wires.ContainsKey(rule.Groups[1].Value) || !wires.ContainsKey(rule.Groups[3].Value))
                {
                    continue;
                }

                wires[rule.Groups[4].Value] = rule.Groups[2].Value switch
                {
                    "AND" => wires[rule.Groups[1].Value] & wires[rule.Groups[3].Value],
                    "OR" => wires[rule.Groups[1].Value] | wires[rule.Groups[3].Value],
                    "XOR" => wires[rule.Groups[1].Value] ^ wires[rule.Groups[3].Value],
                    _ => wires[rule.Groups[4].Value]
                };
            }
        }

        return ExtractValue('z', outputBitKeys.Count, wires);
    }

    private long ExtractValue(char prefix, int order, Dictionary<string, int> wires)
    {
        var outputBitKeys = Enumerable.Range(0, order).Select(i => prefix + i.ToString().PadLeft(2, '0')).ToList();
        
        var outputLow = 0;
        var outputHigh = 0L;
        var shift = 0;
        
        foreach (var bitKey in outputBitKeys.Take(32))
        {
            outputLow += wires[bitKey] << shift;
            ++shift;
        }

        shift = 0;
        foreach (var bitKey in outputBitKeys.Skip(32))
        {
            outputHigh += wires[bitKey] << shift;
            ++shift;
        }

        return (outputHigh << 32) + outputLow;
    }

    private static List<Match> ParseInput(string input, Dictionary<string, int> wires)
    {
        var parts = input.Split(Environment.NewLine + Environment.NewLine);
        foreach (var line in parts[0].Split(Environment.NewLine))
        {
            var parsed = line.Split(": ");
            wires.Add(parsed[0], int.Parse(parsed[1]));
        }

        var rules = new List<Match>();
        foreach (var line in parts[1].Split(Environment.NewLine))
        {
            rules.Add(InputRegex.Match(line));
        }

        return rules;
    }

    public object PartTwo(string input)
    {
        var wires = new Dictionary<string, int>();
        var rules = ParseInput(input, wires);
        var result = (long)PartOne(input);
        var x = ExtractValue('x', 44, wires);
        var y = ExtractValue('y', 44, wires);
        var expected = x + y;

        var expbin = new StringBuilder();
        var actbin = new StringBuilder();

        var badBits = new List<int>();

        for (var i = 0; i <= 45; ++i)
        {
            var expbit = ReadBit(expected, i);
            var actbit = ReadBit(result, i);
            expbin.Append(expbit);
            actbin.Append(actbit);
            if (expbit != actbit)
            {
                Console.WriteLine($"Results differ at bit {i}: expected {expbit}, calculated {actbit}");
                badBits.Add(i);
            }
        }
        
        Console.WriteLine($"Expected:   {expbin} (backwards)");
        Console.WriteLine($"Calculated: {actbin} (backwards)");

        badBits.ForEach(k => FindGeneratorsForOutput('z' + k.ToString().PadLeft(2, '0'), rules));

        Console.WriteLine();
        return 2;
    }

    private static void FindGeneratorsForOutput(string outputKey, List<Match> rules)
    {
        var badGenerators = new HashSet<string>();

        var outputRule = rules.First(r => r.Groups[4].Value == outputKey);
        if (!outputRule.Groups[1].Value.StartsWith('x') && !outputRule.Groups[1].Value.StartsWith('y'))
            badGenerators.Add(outputRule.Groups[1].Value);
        if (!outputRule.Groups[1].Value.StartsWith('x') && !outputRule.Groups[1].Value.StartsWith('y'))
            badGenerators.Add(outputRule.Groups[3].Value);
        // Console.WriteLine(outputRule.Groups[0].Value);

        // Console.WriteLine();
        var newGenerators = 1;
        while (newGenerators > 0)
        {
            var currentSize = badGenerators.Count;
            var tempIterator = new string[badGenerators.Count];
            badGenerators.CopyTo(tempIterator);
            foreach (var key in tempIterator)
            {
                var rule = rules.First(r => r.Groups[4].Value == key);
                if (!rule.Groups[1].Value.StartsWith('x') && !rule.Groups[1].Value.StartsWith('y'))
                    badGenerators.Add(rule.Groups[1].Value);
                if (!rule.Groups[1].Value.StartsWith('x') && !rule.Groups[1].Value.StartsWith('y'))
                    badGenerators.Add(rule.Groups[3].Value);
                //Console.WriteLine(rule.Groups[0].Value);
            }

            newGenerators = badGenerators.Count - currentSize;
        }

        // Console.WriteLine();

        foreach (var key in badGenerators)
        {
            var rule = rules.First(r => r.Groups[4].Value == key);
            // Console.WriteLine(rule.Groups[0].Value);
        }

        Console.WriteLine();

        Console.WriteLine($"{badGenerators.Count} bad generators for {outputKey}");
        // Console.WriteLine(string.Join(',', badGenerators));
    }

    private int ReadBit(long n, int k)
    {
        var temp = n >> k;
        return (int) temp & 1;
    }
}