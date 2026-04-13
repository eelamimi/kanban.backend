namespace Backend.Application.Commands.CommandHandler;

public class MoveIssueCommandHandler(
    IIssueRepository issueRepository,
    IColumnRepository columnRepository)
    : ICommandHandler<MoveIssueCommand>
{
    public async Task Handle(MoveIssueCommand command, CancellationToken token)
    {
        var issue = await issueRepository.GetByIdAsync(command.IssueId, token);
        var sourceColumn = await columnRepository.GetByIdAsync(command.SourceColumnId, true, true, token);
        var targetColumn = await columnRepository.GetByIdAsync(command.TargetColumnId, false, false, token);

        if (!sourceColumn.NextColumns.Contains(targetColumn))
        {
            throw new Exception("Source column has different next columns");
        }

        issue.Column = targetColumn;
        issue.ColumnId = targetColumn.Id;

        if (sourceColumn.Project.Columns.Count - 1 == targetColumn.Position)
        {
            issue.IsDeleted = true;
            issue.ClosedAt = DateTime.UtcNow;
        }
        else
        {
            issue.IsDeleted = false;
            issue.ClosedAt = null;
        }

        await issueRepository.SaveChangesAsync(token);
    }
}
