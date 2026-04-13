namespace Backend.Application.Commands.Command;

public class UpdateRelationCommand : ICommand
{
    public Guid FromColumnId { get; init; }

    public Guid ToColumnId { get; init; }

    public bool IsTransitionAllowed { get; init; }
}