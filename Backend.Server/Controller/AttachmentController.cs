namespace Backend.Server.Controller;

[Authorize]
[Route("api/attachments")]
[ApiController]
public class AttachmentController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("{attachmentId}")]
    public async Task<byte[]> GetAttachmentContent([FromRoute] Guid attachmentId)
    {
        return await mediator.Send(new AttachmentContentQuery
        {
            AttachmentId = attachmentId,
            ProjectId = Request.GetProjectIdFromHeader(),
            UserProfileId = Request.GetUserProfileIdFromHeader(),
        });
    }
}
