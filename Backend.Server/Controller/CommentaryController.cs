namespace Backend.Server.Controller;

[Authorize]
[Route("api/commentaries")]
[ApiController]
public class CommentaryController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("updateContent")]
    public async Task<CommentaryResponse> UpdateContent([FromBody] UpdateCommentaryContentCommand command)
    {
        return await mediator.Send(command);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<StatusCodeResult> DeleteCommentary([FromRoute] Guid id)
    {
        await mediator.Send(new DeleteCommentaryCommand
        {
            Id = id,
            UserProfileId = Request.GetUserProfileIdFromHeader(),
        });

        return Ok();
    }
}
