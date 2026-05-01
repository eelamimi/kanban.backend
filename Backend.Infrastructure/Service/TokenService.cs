namespace Backend.Infrastructure.Service;

public partial class TokenService : ITokenService
{
    private const int TokenLength = 32;
    private const string AllowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public string GenerateToken()
    {
        var tokenChars = new char[TokenLength];
        var randomBytes = new byte[TokenLength];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        for (int i = 0; i < TokenLength; i++)
        {
            int index = randomBytes[i] % AllowedChars.Length;
            tokenChars[i] = AllowedChars[index];
        }

        return new string(tokenChars);
    }

    public bool VerifyToken(string token)
    {
        if (string.IsNullOrEmpty(token) || token.Length != TokenLength)
            return false;

        return TokenRegex().IsMatch(token);
    }

    [GeneratedRegex("^[A-Za-z0-9]+$")]
    private static partial Regex TokenRegex();
}