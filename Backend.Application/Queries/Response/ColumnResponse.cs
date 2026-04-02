namespace Backend.Application.Queries.Response;

public class ColumnResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int Position { get; set; }

    public IEnumerable<IssueResponse> Issues { get; set; } = [];
}
