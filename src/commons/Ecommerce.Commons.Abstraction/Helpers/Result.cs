namespace Ecommerce.Commons.Abstraction.Helpers;

public class Error
{
    public ErrorType Type { get; }
    public string Message { get; }

    public Error(ErrorType type, string message)
    {
        Type = type;
        Message = message;
    }
}

public class Result<T>
{
    public bool IsSuccess { get; }
    public Error? Error { get; }
    public T? Value { get; }

    protected Result(T value)
    {
        IsSuccess = true;
        Value = value;
    }

    protected Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(ErrorType errorType, string errorMessage) => new(new Error(errorType, errorMessage));
}

public class Result
{
    public bool IsSuccess { get; }
    public Error? Error { get; }

    protected Result(bool isSuccess, Error? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true);
    public static Result Failure(ErrorType errorType, string errorMessage) => new(false, new Error(errorType, errorMessage));
}