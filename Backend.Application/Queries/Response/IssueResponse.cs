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

    public bool IsDeleted { get; init; }

    public int NumberInProject { get; init; }

    public DateTime CreatedAt { get; init; }

    public IEnumerable<CommentaryResponse> Commentaries { get; init; } = [];

    public IEnumerable<AttachmentResponse> Attachments { get; init; } = [];
}
