namespace Backend.Application.Queries.Response;

public class IssueResponse
{
    public Guid Id { get; set; }

    public UserResponse Assignee { get; set; }

    public UserResponse Author { get; set; }

    public string Title { get; set; } = string.Empty;

    public IssueType IssueType { get; set; }

    public int StoryPoints { get; set; }

    public IssuePriority IssuePriority { get; set; }

    public bool IsDeleted { get; set; }

    public int NumberInProject { get; set; }

    public IEnumerable<CommentaryResponse> Commentaries { get; set; } = [];
}
