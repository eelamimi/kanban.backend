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
    [Route("{id:guid}")]
    public async Task<StatusCodeResult> DeleteRole([FromRoute] Guid id)
    {
        await mediator.Send(new DeleteRoleCommand
    {
            RoleId = id,
            UserProfileId = Request.GetUserProfileIdFromHeader(),
        });

        return Ok();
    }
}
