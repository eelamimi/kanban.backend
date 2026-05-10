namespace Backend.Application.Map;

public static class ProjectResponseMap
{
    public static ProjectResponse Map(this Project project, bool isNav = false)
    {
        if (isNav)
            return new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                ShortName = project.ShortName,
                Description = project.Description,
                Creator = new(),
                Members = [],
                Columns = []
            };

        var columns = project.Columns?
            .Select(c => c.Map())
            .OrderBy(c => c.Position)
            ?? Enumerable.Empty<ColumnResponse>();

        var members = project.Team?
            .TeamUserProfiles
            .Select(tup => tup.UserProfile.Map())
            ?? [];

        var teamName = project.Team?.Name ?? string.Empty;

        return new ProjectResponse
        {
            Id = project.Id,
            Creator = project.Creator.Map(),
            Description = project.Description,
            Name = project.Name,
            ShortName = project.ShortName,
            TeamName = teamName,
            Members = members,
            Columns = columns
        };
    }
}
