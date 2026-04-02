namespace Backend.Domain.Service;

public interface IJwtService
{
    string GenerateToken(Guid userId, string firstName, string secondName, string email);

    Task<Guid?> GetUserIdFromTokenAsync(string token);

    bool VerifyToken(string token);
}
