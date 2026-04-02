namespace Backend.Domain.Exceptions;

public class ConflictException(string message) : BaseException(message)
{
    public override HttpStatusCode StatusCode { get; } = HttpStatusCode.Conflict;
}