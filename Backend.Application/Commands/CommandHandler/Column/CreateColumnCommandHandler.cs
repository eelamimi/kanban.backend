namespace Backend.Application.Commands.CommandHandler;

public class CreateColumnCommandHandler(
    IProjectRepository projectRepository,
    IColumnRepository columnRepository,
    IColumnRelationRepository columnRelationRepository) : ICommandHandler<CreateColumnCommand, CreateColumnResult>
{
    public async Task<CreateColumnResult> Handle(CreateColumnCommand command, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
            throw new UserInputException("Column name cannot be empty");

        if (await projectRepository.HasColumnName(command.ProjectId, command.Name, token))
            throw new UserInputException("Column with this name already exists");

        var project = await projectRepository.GetByIdAsync(command.ProjectId, token: token);
        var column = new Column
        {
            Name = command.Name,
            Position = command.Position,
            Project = project,
        };
        columnRepository.Add(column);

        if (command.PrevColumnId.HasValue)
        {
            var prevColumn = await columnRepository.GetByIdAsync(command.PrevColumnId.Value, token);
            var prevColumnRelation = new ColumnRelation
            {
                PrevColumn = prevColumn,
                NextColumn = column,
            };
            columnRelationRepository.Add(prevColumnRelation);
        }

        if (command.NextColumnId.HasValue)
        {
            var nextColumn = await columnRepository.GetByIdAsync(command.NextColumnId.Value, token);
            var nextColumnRelation = new ColumnRelation
            {
                PrevColumn = column,
                NextColumn = nextColumn,
            };
            columnRelationRepository.Add(nextColumnRelation);
        }

        await columnRepository.SaveChangesAsync(token);

        return new CreateColumnResult
        {
            ColumnId = column.Id,
        };
    }
}
