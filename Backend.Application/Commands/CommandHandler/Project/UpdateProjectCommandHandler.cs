namespace Backend.Application.Commands.CommandHandler;

public class UpdateProjectCommandHandler(
    IProjectRepository projectRepository) 
    : ICommandHandler<UpdateProjectCommand>
{
    public async Task Handle(UpdateProjectCommand command, CancellationToken token)
    {
        var project = await projectRepository.GetByIdAsync(command.ProjectId, false, false, false, token);

        project.Name = command.Name;
        project.ShortName = command.ShortName;
        project.Description = command.Description;

        await projectRepository.SaveChangesAsync(token);
    }
}
