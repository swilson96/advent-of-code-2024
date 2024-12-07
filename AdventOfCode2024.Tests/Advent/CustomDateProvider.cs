using AdventOfCode2024.Advent;

namespace AdventOfCode2024.Tests.Advent;

public class CustomDateProvider : IDateProvider
{
    private readonly DateOnly _customDate;
    
    public CustomDateProvider(DateOnly customDate)
    {
        _customDate = customDate;
    }

    public DateOnly GetCurrentDate() => _customDate;
}