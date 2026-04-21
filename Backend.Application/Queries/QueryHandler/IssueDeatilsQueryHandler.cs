namespace Backend.Application.Queries.QueryHandler;

public class IssueDeatilsQueryHandler(
    IIssueRepository issueRepository,
    IAttachmentRepository attachmentRepository)
    : ICommandHandler<IssueDeatilsQuery, IssueResponse>
{
    public async Task<IssueResponse> Handle(IssueDeatilsQuery query, CancellationToken token)
    {
        var numberInProject = int.Parse(query.PublicId.Split('-', 2)[1]);
        var issue = await issueRepository.GetByNumberInProjectAndProjectIdsAsync(numberInProject, query.ProjectId, true, token);
        var attachments = await attachmentRepository.GetAllByIssueIdAsync(issue.Id, token);

        return issue.Map(attachments);
    }
}
