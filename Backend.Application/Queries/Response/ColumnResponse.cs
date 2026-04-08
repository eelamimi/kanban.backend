namespace Backend.Application.Queries.Response;

public class ColumnResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Position { get; set; }

    public IEnumerable<Guid> NextColumns { get; set; } = [];

    public IEnumerable<IssueResponse> Issues { get; set; } = [];
}
