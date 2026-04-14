namespace Backend.Application.Commands.Command;

public class CreateColumnCommand : ICommand<ColumnResponse>
{
    public Guid ProjectId { get; init; }

    public Guid? PrevColumnId { get; init; }

    public Guid? NextColumnId { get; init; }

    public string Name { get; init; } = string.Empty;

    public int Position { get; init; } 
}
