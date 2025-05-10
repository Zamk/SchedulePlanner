using CSharpFunctionalExtensions;

namespace Schedule.Core.Models;

public class TelegramHandle : ValueObject
{
    public string Handle { get; }

    private TelegramHandle(string handle)
    {
        Handle = handle;
    }

    public static Result<TelegramHandle> Create(string handle)
    {
        if (string.IsNullOrWhiteSpace(handle))
            return Result.Failure<TelegramHandle>("TelegramHandle must be not null or white space");
        
        if (handle.Length < 5)
            return Result.Failure<TelegramHandle>("TelegramHandle.Length must be greater than 5");

        if (handle.First() is not '@')
            return Result.Failure<TelegramHandle>("TelegramHandle must begin with @ symbol");
        
        var handleObject = new TelegramHandle(handle);

        return Result.Success(handleObject);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Handle;
    }
}