namespace Booking.Domain.ErrorHandling;

public class Result
{
    public bool IsSuccess { get; }
    public Error Error { get; }
    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);
    public static Result Fail(Error error) => new(false, error);
    public static implicit operator Result(Error error) => Fail(error);
}

public class Result<T> : Result
{
    public T Data { get; set; }

    protected Result(bool isSuccess, Error error, T data) : base(isSuccess, error)
    {
        Data = data;
    }

    public static Result<T> Success(T data) => new(true, Error.None, data);
    public new static Result<T> Fail(Error error) => new(false, error, default);
    public static implicit operator Result<T>(T data) => new Result<T>(true, Error.None, data);
    public static implicit operator Result<T>(Error error) => Fail(error);
}

public class Error
{
    public static Error None => new Error("");
    public string Message { get; set; }

    protected Error()
    {
    }

    protected Error(string error) => Message = error;
}