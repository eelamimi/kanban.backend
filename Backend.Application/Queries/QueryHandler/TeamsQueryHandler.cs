namespace Backend.Application.Queries.QueryHandler;

public class TeamsQueryHandler(ITeamUserProfileRepository teamUserProfileRepository)
    : ICommandHandler<TeamsQuery, IEnumerable<TeamResponse>>
{
    public async Task<IEnumerable<TeamResponse>> Handle(TeamsQuery query, CancellationToken token)
    {
        if (query.UserProfileId == query.PersonUserId || 
            await teamUserProfileRepository.IsSameTeam(query.UserProfileId, query.PersonUserId, token))
        {
            var teams = await teamUserProfileRepository.GetTeamsByUserProfileIdAsync(query.PersonUserId, token);

            return teams.Select(tup => tup.Map(tup.Team.Projects));
        }

        throw new ForbiddenException("Different teams");
    }
}
