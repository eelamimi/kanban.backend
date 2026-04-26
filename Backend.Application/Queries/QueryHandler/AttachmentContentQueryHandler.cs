namespace Backend.Application.Queries.QueryHandler;

public class AttachmentContentQueryHandler(
    IAttachmentRepository attachmentRepository,
    ITeamUserProfileRepository teamUserProfileRepository)
    : ICommandHandler<AttachmentContentQuery, byte[]>
{
    public async Task<byte[]> Handle(AttachmentContentQuery query, CancellationToken token)
    {
        if (!await teamUserProfileRepository.IsInProject(query.UserProfileId, query.ProjectId, token))
            throw new ForbiddenException("User is not in project");

        return await attachmentRepository.GetContentByIdAsync(query.AttachmentId, token);
    }
}
