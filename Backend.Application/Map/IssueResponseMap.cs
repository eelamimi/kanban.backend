namespace Backend.Application.Map;

public static class IssueResponseMap
{
    public static IssueResponse Map(this Issue issue, Project? project = null, IEnumerable<Attachment>? attachments = null)
    {
        var commentaries = issue.Commentaries?
            .OrderBy(commentary => commentary.CreatedAt)
            .Select(commentary => commentary.Map())
            ?? [];

        var mappedAttachments = attachments?
            .Select(attachment => attachment.Map()) 
            ?? [];

        var projectName = project?.Name ?? string.Empty;
        var projectShortName = project?.ShortName ?? string.Empty;

        return new IssueResponse
        {
            Id = issue.Id,
            Title = issue.Title,
            StoryPoints = issue.StoryPoints,
            NumberInProject = issue.NumberInProject,
            ProjectName = projectName,
            ProjectShortName = projectShortName,
            IssueType = issue.IssueType,
            IssuePriority = issue.IssuePriority,
            IsDeleted = issue.IsDeleted,
            CreatedAt = issue.CreatedAt,
            ClosedAt = issue.ClosedAt,
            Assignee = issue.Assignee.Map(),
            Author = issue.Author.Map(),
            Commentaries = commentaries,
            Attachments = mappedAttachments,
        };
    }
}
