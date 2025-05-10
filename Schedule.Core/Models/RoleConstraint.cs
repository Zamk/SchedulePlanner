namespace Schedule.Core.Models;

public class RoleConstraint
{
    public long Id { get; }
    public Role RequiredRole { get; }
    public int RequiredCount { get; }
}