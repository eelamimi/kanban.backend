namespace Backend.Application.Map;

public static class TeamDetailsResponseMap
{
    public static TeamDetailsResponse Map(this Team team, IEnumerable<TeamUserProfile> userRolePairs)
    {
        return new TeamDetailsResponse
        {
            Id = team.Id,
            Name = team.Name,
            Roles = team.Roles.Select(r => r.Map()),
            Projects = team.Projects.Select(p => p.Map()),
            UserRolePairs = userRolePairs.Select(tup => tup.Map<UserRolePairResponse>()),
        };
    }
}
