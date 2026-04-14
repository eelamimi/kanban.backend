namespace Backend.Application.Queries.QueryHandler;

public class ProjectDetailsQueryHandler(
    IProjectRepository projectRepository,
    ITeamUserProfileRepository teamUserProfileRepository)
    : ICommandHandler<ProjectDetailsQuery, ProjectResponse>
{
    public async Task<ProjectResponse> Handle(ProjectDetailsQuery query, CancellationToken token)
    {
        if (!await teamUserProfileRepository.IsInProject(query.UserProfileId, query.ProjectId, token))
            throw new ForbiddenException("User is not in project");

        var project = await projectRepository.GetByIdAsync(query.ProjectId, false, true, true, token);

        return project.Map();
    }
}
