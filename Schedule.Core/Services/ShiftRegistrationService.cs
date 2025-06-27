using CSharpFunctionalExtensions;
using Schedule.Core.Abstractions.Interfaces;
using Schedule.Core.Abstractions.Repositories;
using Schedule.Core.Models;
using Schedule.Core.Models.Employees;
using Schedule.Core.Models.Schedules;

namespace Schedule.Core.Services;

public class ShiftRegistrationService :  IShiftRegistrationService
{
    private readonly IShiftRepository _shiftRepository;

    public ShiftRegistrationService(
        IShiftRepository shiftRepository
        )
    {
        _shiftRepository = shiftRepository;
    }
    
    public async Task<Result> TryRegisterEmployeeOnShift(
        WorkingShift workingShift, 
        WorkPoint workPoint,
        Employee employee,
        Role role)
    {
        if (!employee.HasRole(role))
            return Result.Failure("Employee does not have required role");
        
        if (!employee.CanWorkAt(workPoint))
            return Result.Failure("Employee is not allowed to work at this location");
        
        if (!employee.CanWorkAt(workingShift.StartDateTime))
            return Result.Failure("Employee is not available at this time");

        var existingShifts = await _shiftRepository.GetShiftsForEmployee(
            employee.Id, 
            workingShift.StartDateTime, 
            workingShift.EndDateTime);
        
        var hasConflict = existingShifts.Any(existing =>
            TimeRangesOverlap(existing.StartDateTime, 
                existing.EndDateTime, 
                workingShift.StartDateTime, 
                workingShift.EndDateTime));

        if (hasConflict)
            return Result.Failure("Employee already has a conflicting shift");
        
        return workingShift.TryRegisterEmployee(employee, role);
    }
    
    private static bool TimeRangesOverlap(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
    {
        return start1 < end2 && start2 < end1;
    }
}