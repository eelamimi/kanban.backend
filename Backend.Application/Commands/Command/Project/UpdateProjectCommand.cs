namespace Backend.Application.Commands.Command;

public class UpdateProjectCommand : ICommand
{
    public Guid ProjectId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string ShortName { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
}
