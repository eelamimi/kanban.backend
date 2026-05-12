namespace Backend.Application.Queries.Response;

public class IssueResponse
{
    public Guid Id { get; init; }

    public UserResponse Assignee { get; init; }

    public UserResponse Author { get; init; }

    public string Title { get; init; } = string.Empty;

    public IssueType IssueType { get; init; }

    public int StoryPoints { get; init; }

    public IssuePriority IssuePriority { get; init; }

    public bool IsClosed { get; init; }

    public int NumberInProject { get; init; }

    public string ProjectName { get; init; } = string.Empty;

    public string ProjectShortName { get; init; } = string.Empty;

    public DateTime CreatedAt { get; init; }

    public DateTime? ClosedAt { get; init; }

    public IEnumerable<CommentaryResponse> Commentaries { get; init; } = [];

    public IEnumerable<AttachmentResponse> Attachments { get; init; } = [];
}
