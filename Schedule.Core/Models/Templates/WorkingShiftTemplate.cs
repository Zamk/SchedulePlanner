namespace Schedule.Core.Models.Templates;

public class WorkingShiftTemplate
{
    public long Id { get; }
    public DayOfWeek Day { get; }
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }
    public List<RoleConstraint> RoleConstraints { get; }
}