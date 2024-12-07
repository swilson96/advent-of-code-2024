namespace AdventOfCode2024.Advent;

public class AdventCountdown
{
    private readonly IDateProvider _dateProvider;
    private readonly int _year;
    
    public AdventCountdown(IDateProvider dateProvider)
    {
        _dateProvider = dateProvider;
        _year = 2024;
    }
    
    public Boolean IsAdventNow()
    {
        DateOnly today = _dateProvider.GetCurrentDate();
        return today >= new DateOnly(_year, 12, 1) && today <= new DateOnly(_year, 12, 25);
    }

    public int DayOfAdvent() => IsAdventNow() ? _dateProvider.GetCurrentDate().Day : 0;
}