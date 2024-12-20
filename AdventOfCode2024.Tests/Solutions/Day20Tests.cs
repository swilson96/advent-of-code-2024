using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Tests.Solutions;

public class Day20Tests
{
    private const string ExampleInput = @"###############
#...#...#.....#
#.#.#.#.#.###.#
#S#...#.#.#...#
#######.#.#.###
#######.#.#...#
#######.#.###.#
###..E#...#...#
###.#######.###
#...###...#...#
#.#####.#.###.#
#.#...#.#.#...#
#.#.#.#.#.#.###
#...#...#...###
###############";

    [Fact]
    public void ShortestPathNoCheating()
    {
        var result = new Day20().ShortestDistance(ExampleInput);
        
        Assert.Equal(84, result);
    }
    
    [Fact]
    public void PartOneTestSavingTwo()
    {
        var result = new Day20().HowManyCheatsSaveAtLeast(ExampleInput, 2);
        Assert.Equal(44, result);
    }
    
    [Fact]
    public void PartOneTestSavingFour()
    {
        var result = new Day20().HowManyCheatsSaveAtLeast(ExampleInput, 4);
        Assert.Equal(30, result);
    }
    
    [Fact]
    public void PartOneTestSavingTwelve()
    {
        var result = new Day20().HowManyCheatsSaveAtLeast(ExampleInput, 12);
        Assert.Equal(8, result);
    }
    
    [Fact]
    public void PartOneTestSavingSixtyFour()
    {
        var result = new Day20().HowManyCheatsSaveAtLeast(ExampleInput, 64);
        Assert.Equal(1, result);
    }
    
    
    [Fact]
    public void PartOneTestSavingSixtyFive()
    {
        var result = new Day20().HowManyCheatsSaveAtLeast(ExampleInput, 65);
        Assert.Equal(0, result);
    }
    
    [Fact]
    public void PartOneTestSavingSixtySix()
    {
        var result = new Day20().HowManyCheatsSaveAtLeast(ExampleInput, 66);
        Assert.Equal(0, result);
    }

    [Fact]
    public void PartTwoExample()
    {
        var result = new Day20().PartTwo(ExampleInput);
        
        Assert.Equal(0, result);
    }
}