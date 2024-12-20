using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Tests.Solutions;

public class Day16Tests
{
    private const string ExampleInputOne = @"###############
#.......#....E#
#.#.###.#.###.#
#.....#.#...#.#
#.###.#####.#.#
#.#.#.......#.#
#.#.#####.###.#
#...........#.#
###.#.#####.#.#
#...#.....#.#.#
#.#.#.###.#.#.#
#.....#...#.#.#
#.###.#.#.#.#.#
#S..#.....#...#
###############";

    private const string ExampleInputTwo = @"#################
#...#...#...#..E#
#.#.#.#.#.#.#.#.#
#.#.#.#...#...#.#
#.#.#.#.###.#.#.#
#...#.#.#.....#.#
#.#.#.#.#.#####.#
#.#...#.#.#.....#
#.#.#####.#.###.#
#.#.#.......#...#
#.#.###.#####.###
#.#.#...#.....#.#
#.#.#.#####.###.#
#.#.#.........#.#
#.#.#.#########.#
#S#.............#
#################";
    
    [Fact]
    public void PartOneExampleOne()
    {
        var result = new Day16().PartOne(ExampleInputOne);
        
        Assert.Equal(7036, result);
    }
    
    [Fact]
    public void PartOneExampleTwo()
    {
        var result = new Day16().PartOne(ExampleInputTwo);
        
        Assert.Equal(11048, result);
    }

    [Fact]
    public void PartTwoExample()
    {
        var result = new Day16().PartTwo(ExampleInputOne);
        
        Assert.Equal(0, result);
    }
}