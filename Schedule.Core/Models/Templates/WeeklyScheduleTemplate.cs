namespace Schedule.Core.Models.Templates;

public class WeeklyScheduleTemplate
{
    public long Id { get; }
    public string Name { get; }
    public WorkPoint WorkPoint { get; }
    public List<WorkingShiftTemplate> WorkingShifts { get; }
}