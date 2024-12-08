using AdventOfCode2024.Solutions;
using AdventOfCode2024.Solutions.Cartesian;

namespace AdventOfCode2024.Tests.Solutions.Cartesian;

public class PointTests
{
    [Fact]
    public void SimplifyVector_halves() => Assert.Equal(new Point(1, 2), Point.SimplifyVector(new Point(2, 4)));
    
    [Fact]
    public void SimplifyVector_dividesByThree() => Assert.Equal(new Point(1, -3), Point.SimplifyVector(new Point(3, -9)));

    [Fact]
    public void SimplifyVector_dividesByThreeAgain() => Assert.Equal(new Point(-2, 7), Point.SimplifyVector(new Point(-6, 21)));

    [Fact]
    public void SimplifyVector_dividesByThreeYetAgain() => Assert.Equal(new Point(5, 4), Point.SimplifyVector(new Point(15, 12)));
    
    [Fact]
    public void SimplifyVector_dividesByEight() => Assert.Equal(new Point(10, 3), Point.SimplifyVector(new Point(80, 24)));
    
    [Fact]
    public void SimplifyVector_dividesByFourAndThree() => Assert.Equal(new Point(1, 2), Point.SimplifyVector(new Point(12, 24)));
}