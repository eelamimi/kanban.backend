namespace Backend.Application.Queries.QueryHandler;

public class ProjectDetailsQueryHandler(
    IProjectRepository projectRepository,
    ITeamUserProfileRepository teamUserProfileRepository)
    : ICommandHandler<ProjectDetailsQuery, ProjectResponse>
{
    public async Task<ProjectResponse> Handle(ProjectDetailsQuery query, CancellationToken token)
    {
        if (!await teamUserProfileRepository.IsInProject(query.UserProfileId, query.ProjectId, token))
            throw new ForbiddenException("Пользователь не состоит в проекте");

        var project = await projectRepository.GetByIdAsync(
            query.ProjectId,
            includeIssues: true,
            includeTeam: true,
            authorId: query.AuthorId,
            assigneeId: query.AssigneeId,
            token: token);

        return project.Map();
    }
}
