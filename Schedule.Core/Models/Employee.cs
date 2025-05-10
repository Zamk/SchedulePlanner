namespace Schedule.Core.Models;

public class Employee
{
    public long Id { get; }
    public string FullName { get; }
    public List<Role> Roles { get; }
    public List<WorkPoint> AllowedWorkPoints { get; }
}