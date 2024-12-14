using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Tests.Solutions;

public class Day09Tests
{
    private const string ExampleInput = @"2333133121414131402";
    
    [Fact]
    public void PartOneExample()
    {
        var result = new Day09().PartOne(ExampleInput);
        
        Assert.Equal(1928L, result);
    }

    [Fact]
    public void PartTwoExample()
    {
        var result = new Day09().PartTwo(ExampleInput);
        
        Assert.Equal(2858L, result);
    }
}