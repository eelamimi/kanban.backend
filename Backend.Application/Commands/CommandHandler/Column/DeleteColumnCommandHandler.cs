namespace Backend.Application.Commands.CommandHandler;

public class DeleteColumnCommandHandler(
    IColumnRepository columnRepository,
    IProjectRepository projectRepository)
    : ICommandHandler<DeleteColumnCommand>
{
    public async Task Handle(DeleteColumnCommand command, CancellationToken token)
    {
        var column = await columnRepository.GetByIdAsync(command.Id, true, false, false, token);
        var project = await projectRepository.GetByIdAsync(column.ProjectId, false, true, false, token);
        var otherColumns = project.Columns
            .Where(c => c.Id != command.Id)
            .OrderBy(c => c.Position)
            .ToList();

        if (otherColumns.Count <= 3)
            throw new InvalidOperationException("Project can not have less than three columns");

        var firstColumn = otherColumns.First();
        foreach (var issue in column.Issues)
        { 
            issue.ColumnId = firstColumn.Id;
            issue.IsDeleted = false;
            issue.ClosedAt = null;
        }        

        for (int i = 0; i < otherColumns.Count; i++)
        {
            otherColumns[i].Position = i;
            columnRepository.Update(otherColumns[i]);
        }
        columnRepository.Remove(column);

        await columnRepository.SaveChangesAsync(token);
    }
}
