namespace Backend.Server.Controller;

[Authorize]
[Route("api/invites")]
[ApiController]
public class InviteController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<string> CreateInvite([FromBody] GetOrCreateInviteCommand command)
    {
        return await mediator.Send(command);
    }

    [HttpPost]
    [Route("inviteUser")]
    public async Task<Guid> InviteUser([FromBody] AddUserToTeamCommand command)
    {
        command.UserProfileId = Request.GetUserProfileIdFromHeader();

        return await mediator.Send(command);
    }
}
