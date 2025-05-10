namespace Schedule.Core.Models;

public class WorkPoint
{
    public long Id { get; }
    public string Name { get; }
    public TimeOnly WorkDayStart { get; }
    public TimeOnly WorkDayEnd { get; }
}