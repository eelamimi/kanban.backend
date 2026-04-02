namespace Backend.Domain.Exceptions;

public class BaseException : Exception
{
    public virtual HttpStatusCode StatusCode { get; } = HttpStatusCode.InternalServerError;

    public BaseException(string message) : base(message)
    {
    }

    protected BaseException(HttpStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }

    public BaseException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
