namespace Backend.Application.Queries.Response;

public class RegistryOrLoginUserResponse
{
    public Guid UserId { get; init; }

    public Guid UserProfileId { get; init; }

    public string Token { get; init; } = string.Empty;
}
