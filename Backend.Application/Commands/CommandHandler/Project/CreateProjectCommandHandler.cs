namespace Backend.Application.Commands.CommandHandler;

public class CreateProjectCommandHandler(
    IProjectRepository projectRepository,
    IUserProfileRepository userProfileRepository,
    ITeamRepository teamRepository,
    IMediator mediator) : ICommandHandler<CreateProjectCommand, ProjectResponse>
{
    public async Task<ProjectResponse> Handle(CreateProjectCommand command, CancellationToken token)
    {
        if (command.Name.Length >= 16)
            throw new UserInputException("Название проекта должно быть меньше 16 символов");

        var team = await teamRepository.GetByIdAsync(command.TeamId, token);
        var userProfile = await userProfileRepository.GetByIdAsync(command.UserProfileId, token);
        var project = new Project
        {
            Team = team,
            Creator = userProfile,
            Name = command.Name,
            ShortName = command.ShortName,
            Description = command.Description,
        };
        projectRepository.Add(project);
        await projectRepository.SaveChangesAsync(token);

        Guid? prevColumnId = null;
        var columnNames = new[] { "Бэклог", "В работе", "Завершено" };
        for (int i = 0; i < 3; i++)
        {
            var prevColumn = await mediator.Send(new CreateColumnCommand
            {
                ProjectId = project.Id,
                PrevColumnId = prevColumnId,
                Name = columnNames[i],
                Position = i
            }, token);

            prevColumnId = prevColumn.Id;
        }

        return project.Map();
    }
}
