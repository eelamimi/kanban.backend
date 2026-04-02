namespace Backend.Application.Queries.Query;

public class VerifyTokenQuery : ICommand<VerifyTokenResponse>
{
    public string Token { get; init; } = string.Empty;
}
