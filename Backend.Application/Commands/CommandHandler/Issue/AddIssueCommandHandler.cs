namespace Backend.Application.Commands.CommandHandler;

public class AddIssueCommandHandler(
    IIssueRepository issueRepository,
    IProjectRepository projectRepository,
    IUserProfileRepository userProfileRepository,
    ICommentaryRepository commentaryRepository,
    IAttachmentRepository attachmentRepository)
    : ICommandHandler<AddIssueCommand, IssueResponse>
{
    public async Task<IssueResponse> Handle(AddIssueCommand command, CancellationToken token)
    {
        var issueNumber = await issueRepository.GetNextNumberInProjectAsync(command.ProjectId, token);
        var project = await projectRepository.GetByIdAsync(command.ProjectId, false, true, false, token);
        var column = project.Columns.Single(column => column.Position == 0);
        var author = await userProfileRepository.GetByIdAsync(command.AuthorId, token);
        var assignee = await userProfileRepository.GetByIdAsync(command.AssigneeId, token);

        var createdAt = DateTime.UtcNow;

        var issue = new Issue
        {
            ColumnId = column.Id,
            Column = column,
            ProjectId = project.Id,
            Project = project,
            AuthorId = command.AuthorId,
            Author = author,
            AssigneeId = command.AssigneeId,
            Assignee = assignee,
            IssueType = command.IssueType,
            Title = command.Title,
            NumberInProject = issueNumber,
            IssuePriority = command.IssuePriority,
            StoryPoints = command.StoryPoints,
            CreatedAt = createdAt,
        };

        var commentary = new Commentary
        {
            IssueId = issue.Id,
            AuthorId = command.AuthorId,
            Content = command.Description,
            IsDescription = true,
            CreatedAt = createdAt,
            LastEditedAt = createdAt,
        };

        foreach (var file in command.Files)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, token);
            var fileBytes = memoryStream.ToArray();

            var attachment = new Attachment
            {
                IssueId = issue.Id,
                CommentaryId = commentary.Id,
                Content = fileBytes,
                ContentType = file.ContentType,
                FileName = file.FileName,
                Size = file.Length
            };

            attachmentRepository.Add(attachment);
        }

        issueRepository.Add(issue);
        commentaryRepository.Add(commentary);
        await issueRepository.SaveChangesAsync(token);

        return issue.Map();
    }
}
