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

    public static TeamResponse Map(this TeamUserProfile teamUserProfile, IEnumerable<Project> projects)
    {
        return new TeamResponse
        {
            Id = teamUserProfile.Team.Id,
            Name = teamUserProfile.Team.Name,
            Role = teamUserProfile.Role.Map(),
            Projects = projects.Select(p => p.Map(isNav: true))
        };
    }

    public static T Map<T>(this TeamUserProfile teamUserProfile) where T : new()
    {
        return typeof(T) switch
        {
            Type t when t == typeof(TeamResponse) => (T)(object)new TeamResponse
            {
                Id = teamUserProfile.Team.Id,
                Name = teamUserProfile.Team.Name,
                Role = teamUserProfile.Role.Map(),
            },
            Type t when t == typeof(UserRolePairResponse) => (T)(object)new UserRolePairResponse
            {
                Role = teamUserProfile.Role.Map(),
                User = teamUserProfile.UserProfile.Map(),
            },
            _ => throw new NotSupportedException()
        };
    }
}
