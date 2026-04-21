namespace Backend.Application.Queries.Query;

public class IssueDeatilsQuery : ICommand<IssueResponse>
{
    public Guid ProjectId { get; init; }

    public string PublicId { get; init; } = string.Empty;
}
