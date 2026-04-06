namespace Backend.Application.Map;

public static class ProjectResponseMap
{
    public static ProjectResponse Map(this Project project)
    {
        var columns = project.Columns?
            .Select(c => c.Map())
            .OrderBy(c => c.Position)
            ?? Enumerable.Empty<ColumnResponse>();

        var members = project.Team?
            .TeamUserProfiles
            .Select(tup => tup.UserProfile.Map()) 
            ?? [];

        return new ProjectResponse
        {
            Id = project.Id,
            Creator = project.Creator.Map(),
            Description = project.Description,
            Name = project.Name,
            ShortName = project.ShortName,
            Members = members,
            Columns = columns
        };
    }
}
