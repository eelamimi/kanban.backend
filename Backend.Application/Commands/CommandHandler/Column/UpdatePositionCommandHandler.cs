namespace Backend.Application.Commands.CommandHandler;

public class UpdatePositionCommandHandler(
    IColumnRepository columnRepository,
    IIssueRepository issueRepository,
    IProjectRepository projectRepository)
    : ICommandHandler<UpdatePositionCommand>
{
    public async Task Handle(UpdatePositionCommand command, CancellationToken token)
    {
        var column = await columnRepository.GetByIdAsync(command.ColumnId, false, false, false, token);
        var project = await projectRepository.GetByIdAsync(column.ProjectId, true, false, false, token);

        var otherColumns = project.Columns
            .Where(c => c.Id != command.ColumnId)
            .OrderBy(c => c.Position)
            .ToList();

        if (command.NewPosition < 0 || command.NewPosition > otherColumns.Count)
            throw new ArgumentException($"Некорректная позиция: {command.NewPosition}");

        if (command.NewPosition == otherColumns.Count)
        {
            await issueRepository.SetDeletedByColumnIdAsync(column.Id, true, token);
            var lastColumnId = otherColumns.Last().Id;
            await issueRepository.SetDeletedByColumnIdAsync(lastColumnId, false, token);
        }
        else if (column.Position == otherColumns.Count)
        {
            await issueRepository.SetDeletedByColumnIdAsync(column.Id, false, token);
            var lastColumnId = otherColumns.Last().Id;
            await issueRepository.SetDeletedByColumnIdAsync(lastColumnId, true, token);
        }

        for (int i = 0; i < otherColumns.Count; i++)
        {
            if (i >= command.NewPosition)
            {
                otherColumns[i].Position = i + 1;
            }
            else
            {
                otherColumns[i].Position = i;
            }

            columnRepository.Update(otherColumns[i]);
        }

        column.Position = command.NewPosition;
        columnRepository.Update(column);

        await columnRepository.SaveChangesAsync(token);
    }
}
