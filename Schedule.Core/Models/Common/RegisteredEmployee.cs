using CSharpFunctionalExtensions;

namespace Schedule.Core.Models.Common;

public class RegisteredEmployee : ValueObject
{
    public long EmployeeId { get; }
    public long RoleId { get; }
    
    private RegisteredEmployee() {}

    private RegisteredEmployee(long employeeId, long roleId)
    {
        EmployeeId = employeeId;
        RoleId = roleId;
    }

    public static Result<RegisteredEmployee> Create(long employeeId, long roleId)
    {
        return Result.Success(new RegisteredEmployee(employeeId, roleId));
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return EmployeeId;
        yield return RoleId;
    }
}