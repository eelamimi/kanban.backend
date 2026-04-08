namespace Backend.Application.Commands.Command;

public class MoveIssueCommand : ICommand
{
    public Guid IssueId { get; init; }

    public Guid SourceColumnId { get; init; }

    public Guid TargetColumnId { get; init; }
}
