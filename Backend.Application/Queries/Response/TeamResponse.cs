namespace Backend.Application.Queries.Response;

public class TeamResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public RoleResponse Role { get; init; }
}
