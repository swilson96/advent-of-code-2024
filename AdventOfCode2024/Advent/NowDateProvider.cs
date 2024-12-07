namespace AdventOfCode2024.Advent;

public class NowDateProvider : IDateProvider
{
    public DateOnly GetCurrentDate() => DateOnly.FromDateTime(DateTime.Now);
}