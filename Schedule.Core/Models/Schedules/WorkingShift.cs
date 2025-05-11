using CSharpFunctionalExtensions;
using Schedule.Core.Models.Common;

namespace Schedule.Core.Models.Schedules;

public class WorkingShift
{
    private readonly List<RegisteredEmployee> _registeredEmployees;
    
    public long Id { get; }
    public DateTime StartDateTime { get; }
    public DateTime EndDateTime { get; }
    public RoleConstraint RoleConstraint { get; }
    public IReadOnlyCollection<RegisteredEmployee> RegisteredEmployees => _registeredEmployees.AsReadOnly();

    private WorkingShift() {}

    private WorkingShift(DateTime startDateTime, DateTime endDateTime, RoleConstraint roleConstraint)
    {
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        RoleConstraint = roleConstraint;
        _registeredEmployees = new List<RegisteredEmployee>();
    }

    public static Result<WorkingShift> Create(DateTime startDateTime, DateTime endDateTime, RoleConstraint roleConstraint)
    {
        if (startDateTime >= endDateTime)
            return Result.Failure<WorkingShift>("StartDateTime must be less than EndDateTime");

        var shift = new WorkingShift(startDateTime, endDateTime, roleConstraint);

        return Result.Success(shift);
    }
}