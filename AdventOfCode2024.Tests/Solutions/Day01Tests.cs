using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Tests.Solutions;

public class Day01Tests
{
    private const string ExampleInput = @"3   4
4   3
2   5
1   3
3   9
3   3";
    
    [Fact]
    public void PartOneExample()
    {
        var result = new Day01().PartOne(ExampleInput);
        
        Assert.Equal(11, result);
    }

    [Fact]
    public void PartTwoExample()
    {
        var result = new Day01().PartTwo(ExampleInput);
            
        Assert.Equal(31, result);
}
}