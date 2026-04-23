namespace Backend.Application.Queries.QueryHandler;

public class IssueDeatilsQueryHandler(
    IIssueRepository issueRepository,
    IAttachmentRepository attachmentRepository)
    : ICommandHandler<IssueDeatilsQuery, IssueResponse>
{
    public async Task<IssueResponse> Handle(IssueDeatilsQuery query, CancellationToken token)
    {
        Issue issue;
        if (query.IssueId.HasValue)
        {
            issue = await issueRepository.GetByIdAsync(query.IssueId.Value, true, true, token);
        }
        else 
        {
            var numberInProject = int.Parse(query.PublicId.Split('-', 2)[1]);
            issue = await issueRepository.GetByNumberInProjectAndProjectIdsAsync(numberInProject, query.ProjectId, true, token);
        }
        var attachments = await attachmentRepository.GetAllByIssueIdAsync(issue.Id, token);

        return issue.Map(attachments);
    }
}
