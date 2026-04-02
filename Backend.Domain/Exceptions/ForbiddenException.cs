namespace Backend.Domain.Exceptions;

public class ForbiddenException(string message) : BaseException(message)
{
    public override HttpStatusCode StatusCode { get; } = HttpStatusCode.Forbidden;
}