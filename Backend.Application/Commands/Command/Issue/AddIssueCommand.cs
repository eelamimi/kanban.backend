namespace Backend.Application.Commands.Command;

public class AddIssueCommand : ICommand<IssueResponse>
{
    public Guid AssigneeId { get; init; }

    public Guid AuthorId { get; init; }

    public Guid ProjectId { get; init; }

    public string Title { get; init; } = string.Empty;

    public IssueType IssueType { get; init; }

    public int StoryPoints { get; init; }

    public IssuePriority IssuePriority { get; init; }

    public string Description { get; init; } = string.Empty;

    public List<IFormFile> Files { get; set; } = [];
}
