namespace Backend.Application.Queries.Response;

public class IssueResponse
{
    public Guid Id { get; set; }

    public UserResponse Assignee { get; set; }

    public UserResponse Author { get; set; }

    public string PublicId { get; set; }

    public string Title { get; set; }

    public IssueType IssueType { get; set; }

    public int StoryPoints { get; set; }

    public int Priority { get; set; }
}
