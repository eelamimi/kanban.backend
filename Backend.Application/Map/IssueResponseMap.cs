namespace Backend.Application.Map;

public static class IssueResponseMap
{
    public static IssueResponse Map(this Issue issue)
    {
        return new IssueResponse
        {
            Id = issue.Id,
            PublicId = issue.PublicId,
            IssueType = issue.IssueType,
            Priority = issue.Priority,
            StoryPoints = issue.StoryPoints,
            Title = issue.Title,
            Assignee = issue.Assignee.Map(),
            Author = issue.Author.Map(),
        };
    }
}
