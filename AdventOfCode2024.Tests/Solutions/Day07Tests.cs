using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Tests.Solutions;

public class Day07Tests
{
    private const string ExampleInput = @"";
    
    [Fact]
    public void PartOneExample()
    {
        var result = new Day07().PartOne(ExampleInput);
        
        Assert.Equal(0, result);
    }

    [Fact]
    public void PartTwoExample()
    {
        var result = new Day07().PartTwo(ExampleInput);
        
        Assert.Equal(0, result);
    }
}