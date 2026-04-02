namespace Backend.Application.Queries.Response;

public class ProjectResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string ShortName { get; set; }

    public string Description { get; set; }

    public UserResponse Creator { get; set; }

    public IEnumerable<FilterResponse> Filters { get; set; } = [];

    public IEnumerable<ColumnResponse> Columns { get; set; } = [];
}
