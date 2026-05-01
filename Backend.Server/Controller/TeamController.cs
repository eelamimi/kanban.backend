namespace Backend.Server.Controller;

[Authorize]
[Route("api/teams")]
[ApiController]
public class TeamContoller(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<TeamResponse>> GetTeams([FromQuery] Guid personUserId)
    {
        var userProfileId = Request.GetUserProfileIdFromHeader();

        var result = await mediator.Send(new TeamsQuery 
        {
            UserProfileId = userProfileId,
            PersonUserId = personUserId
        });

        return result;
    }

    [HttpGet]
    [Route("{teamId}")]
    public async Task<TeamDetailsResponse> GetTeamDetails([FromRoute] Guid teamId)
    {
        var result = await mediator.Send(new TeamDetailsQuery
        {
            TeamId = teamId,
            UserProfileId = Request.GetUserProfileIdFromHeader()
        });

        return result;
    }

    [HttpPost]
    public async Task<TeamResponse> CreateTeam([FromBody] CreateTeamCommand command)
    {
        command.UserProfileId = Request.GetUserProfileIdFromHeader();

        return await mediator.Send(command);
    }

    [HttpPut]
    public async Task<StatusCodeResult> UpdateTeamName([FromBody] UpdateTeamNameCommand command)
    {
        command.UserProfileId = Request.GetUserProfileIdFromHeader();

        await mediator.Send(command);

        return Ok();
    }

    [HttpDelete]
    [Route("{teamId}/{userProfileId}")]
    public async Task<StatusCodeResult> DeleteUserFromTeam([FromRoute] Guid teamId, [FromRoute] Guid userProfileId)
    {
        await mediator.Send(new DeleteUserFromTeamCommand
        {
            TeamId = teamId,
            UserProfileId = userProfileId,
        });

        return Ok();
    }
}
