namespace Backend.Application.Commands.Command;

public class UpdateColumnNameCommand : ICommand
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;
}
