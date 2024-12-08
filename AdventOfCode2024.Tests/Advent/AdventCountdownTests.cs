using AdventOfCode2024.Advent;

namespace AdventOfCode2024.Tests.Advent;

public class AdventCountdownTests
{
    private const int Year = 2024;
    
    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(13)]
    [InlineData(24)]
    [InlineData(25)]
    public void DatesInAdventyearReturned(int day)
    {
        var sut = new AdventCountdown(new CustomDateProvider(new DateOnly(Year, 12, day)));
        Assert.True(sut.IsAdventNow());
        Assert.Equal(day, sut.DayOfAdvent());
    }
    
    [Theory]
    [InlineData(Year, 3, 3)]
    [InlineData(Year, 11, 1)]
    [InlineData(Year, 11, 30)]
    [InlineData(Year, 12, 26)]
    [InlineData(Year, 12, 30)]
    [InlineData(Year, 1, 1)]
    [InlineData(1999, 12, 1)]
    [InlineData(2023, 12, 1)]
    [InlineData(2025, 12, 24)]
    public void DatesOutsideAdventYearReturnZero(int year, int month, int day)
    {
        var sut = new AdventCountdown(new CustomDateProvider(new DateOnly(year, month, day)));
        Assert.False(sut.IsAdventNow());
        Assert.Equal(0, sut.DayOfAdvent());
    }
}