namespace Schedule.Core.Models;

public class WeeklyScheduleTemplate
{
    public long Id { get; }
    public string Name { get; }
    public List<WorkingShiftTemplate> WorkingShifts { get; }
}