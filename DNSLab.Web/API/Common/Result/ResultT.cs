namespace DNSLab.Web.API.Common.Result;

public class Result<T>
{
    public bool IsSuccess { get; }

    public T? Value { get; }

    public Error? Error { get; }

    private Result(
        bool isSuccess,
        T? value,
        Error? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value)
        => new(true, value, null);

    public static Result<T> Failure(Error error)
        => new(false, default, error);

    public static implicit operator Result<T>(Result result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException(
                "Cannot convert a successful Result to Result<T>.");

        return Failure(result.Error!);
    }
}