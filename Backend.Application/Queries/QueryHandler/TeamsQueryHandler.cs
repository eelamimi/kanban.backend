namespace Backend.Application.Queries.QueryHandler;

public class TeamsQueryHandler(ITeamUserProfileRepository teamUserProfileRepository)
    : ICommandHandler<TeamsQuery, IEnumerable<TeamResponse>>
{
    public async Task<IEnumerable<TeamResponse>> Handle(TeamsQuery query, CancellationToken token)
    {
        if (query.UserProfileId == query.PersonUserId || 
            await teamUserProfileRepository.IsSameTeam(query.UserProfileId, query.PersonUserId, token))
        {
            var teams = await teamUserProfileRepository.GetTeamsByUserProfileIdAsync(query.UserProfileId, token);

            return teams.Select(tup => new TeamResponse
            {
                Id = tup.Team.Id,
                Name = tup.Team.Name,
                Role = new RoleResponse
                {
                    Id = tup.Role.Id,
                    Name = tup.Role.Name
                }
            });
        }

        throw new ForbiddenException("Different teams");
    }
}
