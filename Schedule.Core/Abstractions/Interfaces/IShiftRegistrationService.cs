using CSharpFunctionalExtensions;
using Schedule.Core.Models;
using Schedule.Core.Models.Employees;
using Schedule.Core.Models.Schedules;

namespace Schedule.Core.Abstractions.Interfaces;

public interface IShiftRegistrationService
{
    Task<Result> TryRegisterEmployeeOnShift(WorkingShift workingShift, 
        WorkPoint workPoint,
        Employee employee,
        Role role);
}