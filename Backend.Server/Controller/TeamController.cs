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
}
