namespace Backend.Application.Commands.Command;

public class DeleteColumnCommand : ICommand
{
    public Guid Id { get; init; }
}
