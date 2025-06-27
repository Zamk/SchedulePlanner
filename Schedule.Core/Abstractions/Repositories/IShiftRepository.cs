using Schedule.Core.Models.Schedules;

namespace Schedule.Core.Abstractions.Repositories;

public interface IShiftRepository
{
    Task<IReadOnlyCollection<WorkingShift>> GetShiftsForEmployee(long employeeId, DateTime from, DateTime to);
}