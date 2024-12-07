using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Tests.Solutions;

public class Day03Tests
{
    private const string ExampleInput = @"xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

    private const string ExamplePartTwoInput =
        @"xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
    
    [Fact]
    public void PartOneExample()
    {
        var result = new Day03().PartOne(ExampleInput);
        
        Assert.Equal(161, result);
    }

    [Fact]
    public void PartTwoExample()
    {
        var result = new Day03().PartTwo(ExamplePartTwoInput);
        
        Assert.Equal(48, result);
    }
}