using CSharpFunctionalExtensions;

namespace Schedule.Core.Models.Common;

public class RoleConstraint : ValueObject
{
    public long RequiredRoleId { get; }
    public int RequiredCount { get; }

    private RoleConstraint() {}

    private RoleConstraint(long requiredRoleId, int requiredCount)
    {
        RequiredRoleId = requiredRoleId;
        RequiredCount = requiredCount;
    }

    public static Result<RoleConstraint> Create(long requiredRoleId, int requiredCount)
    {
        if (requiredCount <= 0)
            return Result.Failure<RoleConstraint>("RequiredCount must be greater than 0");
        
        var constraint = new RoleConstraint(requiredRoleId, requiredCount);

        return Result.Success(constraint);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return RequiredRoleId;
        yield return RequiredCount;
    }
}