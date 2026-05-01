namespace Backend.Application.Queries.QueryHandler;

public class TeamDetailsQueryHandler(
    ITeamRepository teamRepository,
    ITeamUserProfileRepository teamUserProfileRepository)
    : ICommandHandler<TeamDetailsQuery, TeamDetailsResponse>
{
    public async Task<TeamDetailsResponse> Handle(TeamDetailsQuery query, CancellationToken token)
    {
        if (!await teamUserProfileRepository.IsInTeam(query.UserProfileId, query.TeamId, token))
            throw new ForbiddenException("Not in this team");

        var team = await teamRepository.GetByIdAsync(query.TeamId, true, true, token);
        var tups = await teamUserProfileRepository.GetUsersByTeamIdAsync(query.TeamId, token);

        return team.Map(tups);
    }
}
