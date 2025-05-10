namespace Schedule.Core.Models;

public class RoleConstraint
{
    public long Id { get; }
    public long RequiredRoleId { get; }
    public int RequiredCount { get; }
}