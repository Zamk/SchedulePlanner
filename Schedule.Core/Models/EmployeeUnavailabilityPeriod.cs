using CSharpFunctionalExtensions;

namespace Schedule.Core.Models;

public class EmployeeUnavailabilityPeriod
{
    public long Id { get; }
    public DateTime From { get; }
    public DateTime To { get; }
    public UnavailabilityReason Reason { get; }
    public string? Comment { get; }

    private EmployeeUnavailabilityPeriod() {}

    private EmployeeUnavailabilityPeriod(DateTime from, DateTime to, UnavailabilityReason reason, string? comment)
    {
        From = from;
        To = to;
        Reason = reason;
        Comment = comment;
    }

    public static Result<EmployeeUnavailabilityPeriod> Create(DateTime from, DateTime to, UnavailabilityReason reason, string? comment)
    {
        if (from >= to)
            return Result.Failure<EmployeeUnavailabilityPeriod>("The from date cannot be later than the to date");

        if (reason is UnavailabilityReason.Other && string.IsNullOrWhiteSpace(comment))
            return Result.Failure<EmployeeUnavailabilityPeriod>("The reason for the unavailability of Other must have a completed comment");

        var period = new EmployeeUnavailabilityPeriod(from, to, reason, comment);

        return Result.Success(period);
    }
    
    public bool IsInteractWith(DateTime dateTime)
    {
        return dateTime >= From && dateTime <= To;
    }

    public bool IsInteractWith(EmployeeUnavailabilityPeriod period)
    {
        return period.To >= From && period.From <= To;
    }
}