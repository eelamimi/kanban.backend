namespace Backend.Application.Map;

public static class ProjectResponseMap
{
    public static ProjectResponse Map(this Project project)
    {
        var columns = project.Columns?
            .Select(c => c.Map())
            .OrderBy(c => c.Position)
            ?? Enumerable.Empty<ColumnResponse>();

        var filters = project.Team?
            .TeamUserProfiles
            .Select(tup => new FilterResponse
            {
                Id = tup.UserProfileId,
                FirstName = tup.UserProfile.FirstName,
                SecondName = tup.UserProfile.SecondName,
            }) ?? [];

        return new ProjectResponse
        {
            Id = project.Id,
            Creator = project.Creator.Map(),
            Description = project.Description,
            Name = project.Name,
            ShortName = project.ShortName,
            Filters = filters,
            Columns = columns
        };
    }
}
