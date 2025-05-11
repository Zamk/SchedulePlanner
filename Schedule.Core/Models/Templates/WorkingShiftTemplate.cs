using CSharpFunctionalExtensions;
using Schedule.Core.Models.Common;

namespace Schedule.Core.Models.Templates;

public class WorkingShiftTemplate
{
    public long Id { get; }
    public DayOfWeek Day { get; }
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }
    public RoleConstraint RoleConstraint { get; }
    
    private WorkingShiftTemplate() {}

    private WorkingShiftTemplate(DayOfWeek day, TimeOnly startTime, TimeOnly endTime, RoleConstraint roleConstraint)
    {
        Day = day;
        StartTime = startTime;
        EndTime = endTime;
        RoleConstraint = roleConstraint;
    }

    public static Result<WorkingShiftTemplate> Create(DayOfWeek day, TimeOnly startTime, TimeOnly endTime, RoleConstraint roleConstraint)
    {
        if (startTime >= endTime)
            return Result.Failure<WorkingShiftTemplate>("StartTime must be less than EndTime");
        
        var workingShiftTemplate = new WorkingShiftTemplate(day, startTime, endTime, roleConstraint);

        return Result.Success(workingShiftTemplate);
    }
}