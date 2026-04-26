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
}
