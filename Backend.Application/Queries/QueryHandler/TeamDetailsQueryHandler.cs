namespace Backend.Application.Queries.QueryHandler;

public class TeamDetailsQueryHandler(
    ITeamRepository teamRepository,
    IProjectRepository projectRepository,
    ITeamUserProfileRepository teamUserProfileRepository)
    : ICommandHandler<TeamDetailsQuery, TeamDetailsResponse>
{
    public async Task<TeamDetailsResponse> Handle(TeamDetailsQuery query, CancellationToken token)
    {
        if (!await teamUserProfileRepository.IsInTeam(query.UserProfileId, query.TeamId, token))
            throw new ForbiddenException("Not in this team");

        var team = await teamRepository.GetByIdAsync(query.TeamId, token);
        var tups = await teamUserProfileRepository.GetUsersByTeamIdAsync(query.TeamId, token);
        var projects = await projectRepository.GetAllByTeamIdAsync(query.TeamId, token);

        return new TeamDetailsResponse
        {
            Name = team.Name,
            Projects = projects.Select(p => p.Map()),
            UserRolePairs = tups.Select(tup =>
                new UserRolePair
                {
                    Role = tup.Role.Map(),
                    User = tup.UserProfile.Map()
                }),
        };
    }
}
