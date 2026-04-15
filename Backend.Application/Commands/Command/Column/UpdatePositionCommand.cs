namespace Backend.Application.Commands.Command;

public class UpdatePositionCommand : ICommand
{
    public Guid ColumnId { get; init; }

    public int NewPosition { get; init; }
}
