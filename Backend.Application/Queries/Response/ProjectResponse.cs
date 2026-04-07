namespace Backend.Application.Queries.Response;

public class ProjectResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string ShortName { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public UserResponse Creator { get; init; }

    public IEnumerable<UserResponse> Members { get; init; } = [];

    public IEnumerable<ColumnResponse> Columns { get; init; } = [];
}
