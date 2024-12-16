using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Tests.Solutions;

public class Day06Tests
{
    private const string ExampleInput = @"....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...";
    
    [Fact]
    public void PartOneExample()
    {
        var result = new Day06().PartOne(ExampleInput);
        
        Assert.Equal(41, result);
    }

    [Fact]
    public void PartTwoExample()
    {
        var result = new Day06().PartTwo(ExampleInput);
        
        Assert.Equal(0, result);
    }
}