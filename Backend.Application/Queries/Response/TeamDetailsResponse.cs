namespace Backend.Application.Queries.Response;

public class TeamDetailsResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public IEnumerable<ProjectResponse> Projects { get; init; } = [];

    public IEnumerable<UserRolePair> UserRolePairs { get; init; } = [];
}
