namespace Backend.Application.Map;

public static class TeamResponseMap
{
    public static TeamResponse Map(this Team team, Role role)
    {
        return new TeamResponse
        {
            Id = team.Id,
            Name = team.Name,
            Role = role.Map(),
        };
    }

    public static TeamResponse Map(this TeamUserProfile teamUserProfile)
    {
        return new TeamResponse
        {
            Id = teamUserProfile.Team.Id,
            Name = teamUserProfile.Team.Name,
            Role = teamUserProfile.Role.Map(),
        };
    }
}
