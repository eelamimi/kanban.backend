namespace Backend.Server.Controller;

[Authorize]
[Route("api/roles")]
[ApiController]
public class RoleContoller(IMediator mediator) : ControllerBase
{
    [HttpPut]
    public async Task<TeamDetailsResponse> UpdateRole([FromBody] UpdateRoleCommand command)
    {
        command.UserProfileId = Request.GetUserProfileIdFromHeader();

        return await mediator.Send(command);
    }

    [HttpDelete]
    public async Task<StatusCodeResult> DeleteRole([FromBody] DeleteRoleCommand command)
    {
        command.UserProfileId = Request.GetUserProfileIdFromHeader();

        await mediator.Send(command);

        return Ok();
    }
}
