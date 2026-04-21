namespace Backend.Application.Queries.QueryHandler;

public class IssueDeatilsQueryHandler(
    IIssueRepository issueRepository,
    IAttachmentRepository attachmentRepository)
    : ICommandHandler<IssueDeatilsQuery, IssueResponse>
{
    public async Task<IssueResponse> Handle(IssueDeatilsQuery command, CancellationToken token)
    {
        var numberInProject = int.Parse(command.PublicId.Split('-', 1)[1]);
        var issue = await issueRepository.GetByNumberInProjectAndProjectIdsAsync(numberInProject, command.ProjectId, true, token);
        var attachments = await attachmentRepository.GetAllByIssueIdAsync(issue.Id, token);

        return issue.Map();
    }
}
