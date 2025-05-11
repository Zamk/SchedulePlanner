using CSharpFunctionalExtensions;

namespace Schedule.Core.Models.Schedules;

public class WeeklySchedule
{
    private readonly List<WorkingShift> _workingShifts;
    
    public long Id { get; }
    public DateOnly WeekStartDate { get; }
    public WorkPoint WorkPoint { get; }
    public IReadOnlyCollection<WorkingShift> WorkingShifts => _workingShifts.AsReadOnly();
    
    private WeeklySchedule() {}

    private WeeklySchedule(DateOnly weekStartDate, WorkPoint workPoint, IEnumerable<WorkingShift> workingShifts)
    {
        WeekStartDate = weekStartDate;
        WorkPoint = workPoint;
        _workingShifts = workingShifts.ToList();
    }

    public static Result<WeeklySchedule> Create(DateOnly weekStartDate, WorkPoint workPoint, IEnumerable<WorkingShift> workingShifts)
    {
        return Result.Success(new WeeklySchedule(weekStartDate, workPoint, workingShifts));
    }
}