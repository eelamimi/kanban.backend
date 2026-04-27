namespace Backend.Application.Commands.CommandHandler;

public class UpdateIssueCommandHandler(
    IIssueRepository issueRepository,
    ICommentaryRepository commentaryRepository,
    IUserProfileRepository userProfileRepository,
    IMediator mediator) :
    ICommandHandler<UpdateIssueCommand, IssueResponse>
{
    public async Task<IssueResponse> Handle(UpdateIssueCommand command, CancellationToken token)
    {
        var issue = await issueRepository.GetByIdAsync(command.Id, false, false, token);
        var author = await userProfileRepository.GetByIdAsync(command.AuthorId, token);
        var assignee = await userProfileRepository.GetByIdAsync(command.AssigneeId, token);
        var description = await commentaryRepository.GetDescriptionAsync(command.Id, token);

        issue.Assignee = assignee;
        issue.AssigneeId = assignee.Id;
        issue.Author = author;
        issue.AuthorId = author.Id;

        issue.Title = command.Title;
        issue.StoryPoints = command.StoryPoints;
        issue.IssuePriority = command.IssuePriority;
        issue.IssueType = command.IssueType;
        
        description.Content = command.Description;

        await mediator.Send(new CreateAttachmentsCommand
        {
            IssueId = issue.Id,
            CommentaryId = description.Id,
            Files = command.Files,
            Save = false
        }, token);

        issueRepository.Update(issue);
        commentaryRepository.Update(description);
        await issueRepository.SaveChangesAsync(token);

        return await mediator.Send(new IssueDeatilsQuery
        {
            IssueId = issue.Id
        }, token);
    }
}
