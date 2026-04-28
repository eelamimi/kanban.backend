namespace Backend.Application.Queries.QueryHandler;

public class AttachmentContentQueryHandler(
    IFileStorageService fileStorageService,
    IAttachmentRepository attachmentRepository,
    ITeamUserProfileRepository teamUserProfileRepository)
    : ICommandHandler<AttachmentContentQuery, byte[]>
{
    public async Task<byte[]> Handle(AttachmentContentQuery query, CancellationToken token)
    {
        if (!await teamUserProfileRepository.IsInProject(query.UserProfileId, query.ProjectId, token))
            throw new ForbiddenException("User is not in project");

        var contentPath = await attachmentRepository.GetContentPathByIdAsync(query.AttachmentId, token);
        
        var fileBytes = await fileStorageService.GetFileAsync(contentPath, token);

        return fileBytes;
    }
}
