namespace Backend.Domain.Service;

public interface ITokenService
{
    public string GenerateToken();

    public bool VerifyToken(string token);
}
