namespace Backend.Server.Controller;

[Authorize]
[Route("api/invites")]
[ApiController]
public class InviteController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<string> CreateInvite([FromForm] GetOrCreateInviteCommand command)
    {
        return await mediator.Send(command);
    }
}
