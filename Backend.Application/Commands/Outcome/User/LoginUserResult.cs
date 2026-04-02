namespace Backend.Application.Commands.Outcome;

public class LoginUserResult
{
    public Guid UserId { get; init; }

    public Guid UserProfileId { get; init; }

    public string Token { get; init; } = string.Empty;
}
