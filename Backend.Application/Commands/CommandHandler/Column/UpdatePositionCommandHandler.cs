namespace Backend.Application.Commands.CommandHandler;

public class UpdatePositionCommandHandler(
    IColumnRepository columnRepository,
    IProjectRepository projectRepository)
    : ICommandHandler<UpdatePositionCommand>
{
    public async Task Handle(UpdatePositionCommand command, CancellationToken token)
    {
        var column = await columnRepository.GetByIdAsync(command.ColumnId, false, true, token);
        var project = await projectRepository.GetByIdAsync(column.ProjectId, true, false, false, token);

        var otherColumns = project.Columns
            .Where(c => c.Id != command.ColumnId)
            .OrderBy(c => c.Position)
            .ToList();

        if (command.NewPosition < 0 || command.NewPosition > otherColumns.Count)
            throw new ArgumentException($"Некорректная позиция: {command.NewPosition}");

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
