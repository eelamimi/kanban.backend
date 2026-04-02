namespace Backend.Application.Queries.QueryHandler;

public class VerifyTokenQueryHandler(IJwtService jwtService) 
    : ICommandHandler<VerifyTokenQuery, VerifyTokenResponse>
{
    public async Task<VerifyTokenResponse> Handle(VerifyTokenQuery query, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(query.Token))
            throw new ForbiddenException("Token to verify is empty");

        return new VerifyTokenResponse
        {
            IsValid = jwtService.VerifyToken(query.Token)
        };
    }
}
