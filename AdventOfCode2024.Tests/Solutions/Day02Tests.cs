using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Tests.Solutions;

public class Day02Tests
{
    private const string ExampleInput = @"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9";
    
    [Fact]
    public void PartOneExample()
    {
        var result = new Day02().PartOne(ExampleInput);
        
        Assert.Equal(2, result);
    }

    [Fact]
    public void PartTwoExample()
    {
        var result = new Day02().PartTwo(ExampleInput);
        
        Assert.Equal(4, result);
    }
}