namespace Backend.Application.Commands.Command;

public class CreateProjectCommand : ICommand<CreateProjectResult>
{
    public Guid TeamId { get; init; }

    public Guid UserProfileId { get; init; }

    public string Name { get; init; } = string.Empty;
 
    public string ShortName { get; init; } = string.Empty;
    
    public string Description { get; init; } = string.Empty;
}
