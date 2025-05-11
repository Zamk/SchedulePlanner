using CSharpFunctionalExtensions;

namespace Schedule.Core.Models.Employees;

public class Employee
{
    private readonly List<Role> _roles;
    private readonly List<WorkPoint> _allowedWorkPoints;
    private readonly List<EmployeeUnavailabilityPeriod> _unavailabilityPeriods;
    
    public long Id { get; }
    public string FullName { get; }
    public Phone MobilePhone { get; }
    public TelegramHandle TelegramHandle { get; }
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();
    public IReadOnlyCollection<WorkPoint> AllowedWorkPoints => _allowedWorkPoints.AsReadOnly();

    public IReadOnlyCollection<EmployeeUnavailabilityPeriod> UnavailabilityPeriods =>
        _unavailabilityPeriods.AsReadOnly();
    
    private Employee() {}
    
    private Employee(string fullName, Phone phone, TelegramHandle telegramHandle, IEnumerable<Role> roles, IEnumerable<WorkPoint> workPoints)
    {
        FullName = fullName;
        MobilePhone = phone;
        TelegramHandle = telegramHandle;
        _roles = roles.ToList();
        _allowedWorkPoints = workPoints.ToList();
        _unavailabilityPeriods = new List<EmployeeUnavailabilityPeriod>();
    }

    public static Result<Employee> Create(string fullName, Phone phone, TelegramHandle telegramHandle, IEnumerable<Role> roles, IEnumerable<WorkPoint> workPoints)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return Result.Failure<Employee>("FullName should be not null or white space");
        
        if (roles is null || !roles.Any())
            return Result.Failure<Employee>("Roles must contain at least one role");
        
        if (workPoints is null || !workPoints.Any())
            return Result.Failure<Employee>("WorkPoints must contain at least one WorkPoint");

        var employee = new Employee(fullName, phone, telegramHandle, roles, workPoints);

        return Result.Success(employee);
    }

    public bool CanWorkAt(WorkPoint workPoint)
    {
        return _allowedWorkPoints.Any(w => w.Id == workPoint.Id);
    }

    public Result TryAddWorkPoint(WorkPoint workPoint)
    {
        if (_allowedWorkPoints.Any(w => w.Id == workPoint.Id))
            return Result.Failure("WorkPoint already added");
        
        _allowedWorkPoints.Add(workPoint);
        return Result.Success();
    }

    public Result TryRemoveWorkPoint(WorkPoint workPoint)
    {
        var existingWorkPoint = _allowedWorkPoints.FirstOrDefault(w => w.Id == workPoint.Id);
        
        if (existingWorkPoint is null)
            return Result.Failure("WorkPoint not found");

        _allowedWorkPoints.Remove(existingWorkPoint);
        return Result.Success();
    }
    
    public bool HasRole(Role role)
    {
        return _roles.Any(r => r.Id == role.Id);
    }

    public Result TryAddRole(Role role)
    {
        if (_roles.Any(r => r.Id == role.Id))
            return Result.Failure("Given role already added");
        
        _roles.Add(role);
        return Result.Success();
    }

    public Result TryRemoveRole(Role role)
    {
        var existingRole = _roles.FirstOrDefault(r => r.Id == role.Id);
        
        if (existingRole is null)
            return Result.Failure("Role not found");

        _roles.Remove(existingRole);
        return Result.Success();
    }

    public bool CanWorkAt(DateTime dateTime)
    {
        return _unavailabilityPeriods.All(u => !u.IsInteractWith(dateTime));
    }

    public Result TryAddUnavailabilityPeriod(EmployeeUnavailabilityPeriod period)
    {
        if (_unavailabilityPeriods.Any(u => u.Id == period.Id))
            return Result.Failure("Given period already added");
        
        if (_unavailabilityPeriods.Any(u => u.IsInteractWith(period)))
            return Result.Failure("Given period is interact with one or more exisited period");
        
        _unavailabilityPeriods.Add(period);
        return Result.Success();
    }

    public Result TryRemoveUnavailabilityPeriod(EmployeeUnavailabilityPeriod period)
    {
        var existingPeriod = _unavailabilityPeriods.FirstOrDefault(u => u.Id == period.Id);

        if (existingPeriod is null)
            return Result.Failure("Period not found");

        _unavailabilityPeriods.Remove(existingPeriod);
        return Result.Success();
    }
}