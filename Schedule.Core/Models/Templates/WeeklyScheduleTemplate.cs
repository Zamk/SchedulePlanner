using CSharpFunctionalExtensions;
using Schedule.Core.Models.Schedules;

namespace Schedule.Core.Models.Templates;

public class WeeklyScheduleTemplate
{
    private readonly List<WorkingShiftTemplate> _workingShiftTemplates;

    public long Id { get; }
    public string Name { get; }
    public WorkPoint WorkPoint { get; }
    public bool IsEnabled { get; private set; }

    public IReadOnlyCollection<WorkingShiftTemplate> WorkingShiftTemplates => _workingShiftTemplates.AsReadOnly();
    
    private WeeklyScheduleTemplate() {}

    private WeeklyScheduleTemplate(string name, WorkPoint workPoint)
    {
        Name = name;
        WorkPoint = workPoint;
        _workingShiftTemplates = new List<WorkingShiftTemplate>();
        IsEnabled = false;
    }

    public static Result<WeeklyScheduleTemplate> Create(string name, WorkPoint workPoint)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<WeeklyScheduleTemplate>("Name should be not null or white space");

        var template = new WeeklyScheduleTemplate(name, workPoint);
        
        return Result.Success(template);
    }

    public Result TryAddWorkingShiftTemplate(WorkingShiftTemplate template)
    {
        if (_workingShiftTemplates.Any(ws => ws.Id == template.Id))
            return Result.Failure("WorkingShiftTemplate already added");

        _workingShiftTemplates.Add(template);
        
        return Result.Success();
    }

    public Result TryRemoveWorkingShiftTemplate(WorkingShiftTemplate template)
    {
        var existingTemplate = _workingShiftTemplates.FirstOrDefault(ws => ws.Id == template.Id);

        if (existingTemplate is null)
            return Result.Failure("WorkingShiftTemplate not found");

        _workingShiftTemplates.Remove(existingTemplate);

        return Result.Success();
    }

    public Result<WeeklySchedule> TryCreateSchedule(DateOnly weekStartDate)
    {
        var workingShifts = new List<WorkingShift>();

        if (weekStartDate.DayOfWeek is not DayOfWeek.Monday)
            return Result.Failure<WeeklySchedule>("WeekStartDate must be Monday");
        
        foreach (var shiftTemplate in _workingShiftTemplates)
        {
            var shiftStartDay = new DateTime(weekStartDate, TimeOnly.MinValue) 
                                 + TimeSpan.FromDays((int)shiftTemplate.Day);

            var shiftStart = new DateTime(shiftStartDay.Year, shiftStartDay.Month, shiftStartDay.Day,
                shiftTemplate.StartTime.Hour, shiftTemplate.StartTime.Minute, shiftTemplate.StartTime.Second);
            var shiftEnd = shiftStart + shiftTemplate.Duration;
            
            var workingShiftResult = WorkingShift.Create(shiftStart, shiftEnd, shiftTemplate.RoleConstraint);

            if (workingShiftResult.IsFailure)
                return Result.Failure<WeeklySchedule>(workingShiftResult.Error);
            
            workingShifts.Add(workingShiftResult.Value);
        }

        return WeeklySchedule.Create(weekStartDate, WorkPoint, workingShifts);
    }

    public void Enable()
    {
        IsEnabled = true;
    }

    public void Disable()
    {
        IsEnabled = false;
    }
}