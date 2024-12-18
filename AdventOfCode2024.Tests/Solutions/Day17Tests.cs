using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Tests.Solutions;

public class Day17Tests
{
    private const string ExampleInput = @"Register A: 729
Register B: 0
Register C: 0

Program: 0,1,5,4,3,0";
    
    [Fact]
    public void PartOneMainExample()
    {
        var result = new Day17().PartOne(ExampleInput);
        
        Assert.Equal("4,6,3,5,6,3,5,2,1,0", result);
    }
    
    [Fact]
    public void PartOneExample2()
    {
        var result = new Day17().PartOne(@"Register A: 10
Register B: 0
Register C: 0

Program: 5,0,5,1,5,4");
        
        Assert.Equal("0,1,2", result);
    }
    
    [Fact]
    public void PartOneExample3()
    {
        var result = new Day17().PartOne(@"Register A: 2024
Register B: 0
Register C: 0

Program: 0,1,5,4,3,0");
        
        Assert.Equal("4,2,5,6,7,7,7,7,3,1,0", result);
    }

    [Fact]
    public void PartTwoExample()
    {
        var result = new Day17().PartTwo(@"Register A: 2024
Register B: 0
Register C: 0

Program: 0,3,5,4,3,0");
        
        Assert.Equal(117440, result);
    }
}