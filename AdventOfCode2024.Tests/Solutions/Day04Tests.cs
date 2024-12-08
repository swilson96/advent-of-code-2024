using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Tests.Solutions;

public class Day04Tests
{
    private const string ExampleInput = @"MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX";
    
    [Fact]
    public void PartOneExample()
    {
        var result = new Day04().PartOne(ExampleInput);
        
        Assert.Equal(18, result);
    }

    [Fact]
    public void PartTwoExample()
    {
        var result = new Day04().PartTwo(ExampleInput);
        
        Assert.Equal(0, result);
    }
}