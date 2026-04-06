namespace Backend.Application.Map;

public static class IssueResponseMap
{
    public static IssueResponse Map(this Issue issue)
    {
        return new IssueResponse
        {
            Id = issue.Id,
            Title = issue.Title,
            StoryPoints = issue.StoryPoints,
            NumberInProject = issue.NumberInProject,
            IssueType = issue.IssueType,
            IssuePriority = issue.IssuePriority,
            IsDeleted = issue.IsDeleted,
            Assignee = issue.Assignee.Map(),
            Author = issue.Author.Map(),
        };
    }
}
