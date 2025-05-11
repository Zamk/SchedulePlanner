using CSharpFunctionalExtensions;
using Schedule.Core.Models.Common;

namespace Schedule.Core.Models.Templates;

public class WorkingShiftTemplate
{
    public long Id { get; }
    public DayOfWeek Day { get; }
    public TimeOnly StartTime { get; }
    public TimeSpan Duration { get; }
    public RoleConstraint RoleConstraint { get; }
    
    private WorkingShiftTemplate() {}

    private WorkingShiftTemplate(DayOfWeek day, TimeOnly startTime, TimeSpan duration, RoleConstraint roleConstraint)
    {
        Day = day;
        StartTime = startTime;
        Duration = duration;
        RoleConstraint = roleConstraint;
    }

    public static Result<WorkingShiftTemplate> Create(DayOfWeek day, TimeOnly startTime, TimeSpan duration, RoleConstraint roleConstraint)
    {
        var workingShiftTemplate = new WorkingShiftTemplate(day, startTime, duration, roleConstraint);

        return Result.Success(workingShiftTemplate);
    }
}