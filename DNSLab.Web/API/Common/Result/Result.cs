namespace DNSLab.Web.API.Common.Result;

public class Result
{
    public bool IsSuccess { get; }

    public Error? Error { get; }

    private Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success()
        => new(true, null);

    public static Result Failure(Error error)
        => new(false, error);
}