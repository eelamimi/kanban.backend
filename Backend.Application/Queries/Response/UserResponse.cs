namespace Backend.Application.Queries.Response;

public class UserResponse
{
    public Guid Id { get; set; }

    public string FirstName { get; init; } = string.Empty;
    
    public string SecondName { get; init; } = string.Empty;
    
    public string Email { get; init; } = string.Empty;

    public DateTime CreatedAt { get; init; }

    public byte[] Avatar { get; init; } = [];
}
