namespace Backend.Application.Commands.CommandHandler;

public class CreateColumnCommandHandler(
    IProjectRepository projectRepository,
    IColumnRepository columnRepository,
    IColumnRelationRepository columnRelationRepository) : ICommandHandler<CreateColumnCommand, ColumnResponse>
{
    public async Task<ColumnResponse> Handle(CreateColumnCommand command, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
            throw new UserInputException("Column name cannot be empty");

        if (await projectRepository.HasColumnName(command.ProjectId, command.Name, token))
            throw new UserInputException("Column with this name already exists");

        var project = await projectRepository.GetByIdAsync(command.ProjectId, true, token: token);
        var column = new Column
        {
            Name = command.Name,
            Position = command.Position,
            Project = project,
        };
        columnRepository.Add(column);

        foreach (var col in project.Columns.Where(col => col.Position >= column.Position && col.Id != column.Id))
        {
            col.Position++;
            columnRepository.Update(col);
        }

        if (command.PrevColumnId.HasValue)
        {
            var prevColumn = await columnRepository.GetByIdAsync(command.PrevColumnId.Value, token: token);
            var prevColumnRelation = new ColumnRelation
            {
                PrevColumn = prevColumn,
                NextColumn = column,
            };
            columnRelationRepository.Add(prevColumnRelation);
        }

        if (command.NextColumnId.HasValue)
        {
            var nextColumn = await columnRepository.GetByIdAsync(command.NextColumnId.Value, token: token);
            var nextColumnRelation = new ColumnRelation
            {
                PrevColumn = column,
                NextColumn = nextColumn,
            };
            columnRelationRepository.Add(nextColumnRelation);
        }

        await columnRepository.SaveChangesAsync(token);

        return new ColumnResponse
        {
            Id = column.Id,
            Name = column.Name,
            Position = column.Position,
            Issues = [],
            NextColumns = [],
        };
    }
}
