namespace Backend.Application.Queries.Query;

public class ProjectDetailsQuery : ICommand<ProjectResponse>
{
    public Guid UserProfileId { get; init; }

    public Guid ProjectId { get; init; }

    public Guid? AuthorId { get; init; } = null;

    public Guid? AssigneeId { get; init; } = null;
}
