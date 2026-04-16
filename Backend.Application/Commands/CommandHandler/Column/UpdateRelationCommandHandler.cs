namespace Backend.Application.Commands.CommandHandler;

public class UpdateRelationCommandHandler(
    IColumnRepository columnRepository,
    IColumnRelationRepository columnRelationRepository) 
    : ICommandHandler<UpdateRelationCommand>
{
    public async Task Handle(UpdateRelationCommand command, CancellationToken token)
    {
        var nextColumn = await columnRepository.GetByIdAsync(command.ToColumnId, token: token);
        var prevColumn = await columnRepository.GetByIdAsync(command.FromColumnId, token: token);

        if (command.IsTransitionAllowed)
            columnRelationRepository.Add(new ColumnRelation
            {
                NextColumnId = nextColumn.Id,
                NextColumn = nextColumn,
                PrevColumnId = prevColumn.Id,
                PrevColumn = prevColumn,
            });
        else
            columnRelationRepository.Remove(new ColumnRelation
            {
                NextColumnId = nextColumn.Id,
                NextColumn = nextColumn,
                PrevColumnId = prevColumn.Id,
                PrevColumn = prevColumn,
            });

        await columnRepository.SaveChangesAsync(token);
    }
}
