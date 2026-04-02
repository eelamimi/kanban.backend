namespace Backend.Domain.Exceptions;

public class UserInputException : BaseException
{
    public override HttpStatusCode StatusCode { get; } = HttpStatusCode.BadRequest;

    public UserInputException(string message) : base(message)
    {
    }

    public UserInputException(string message, Exception innerException) : base(message, innerException)
    {
    }
}