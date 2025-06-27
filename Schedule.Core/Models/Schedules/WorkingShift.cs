using CSharpFunctionalExtensions;
using Schedule.Core.Models.Common;
using Schedule.Core.Models.Employees;

namespace Schedule.Core.Models.Schedules;

public class WorkingShift
{
    private readonly List<RegisteredEmployee> _registeredEmployees;
    
    public long Id { get; }
    public DateTime StartDateTime { get; }
    public DateTime EndDateTime { get; }
    public RoleConstraint RoleConstraint { get; }
    public IReadOnlyCollection<RegisteredEmployee> RegisteredEmployees => _registeredEmployees.AsReadOnly();

    public bool IsRegistrationAvailable => RoleConstraint.RequiredCount > _registeredEmployees.Count;
    
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

    public Result TryRegisterEmployee(Employee employee, Role role)
    {
        if (!IsRegistrationAvailable)
            return Result.Failure($"Registration is not available");
        
        if (_registeredEmployees.Any(e => e.EmployeeId == employee.Id))
            return Result.Failure("Employee already registered on this shift");
        
        if (RoleConstraint.RequiredRoleId != role.Id)
            return Result.Failure("Role does not match the shift's required role");
        
        var registration = RegisteredEmployee.Create(employee.Id, role.Id);
        
        if (registration.IsFailure)
            return Result.Failure(registration.Error);
        
        _registeredEmployees.Add(registration.Value);
        
        return Result.Success();
    }
}