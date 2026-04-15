namespace Backend.Application.Commands.CommandHandler;

public class UpdateColumnNameCommandHandler(
    IColumnRepository columnRepository)
    : ICommandHandler<UpdateColumnNameCommand>
{
    public async Task Handle(UpdateColumnNameCommand command, CancellationToken token)
    {
        var column = await columnRepository.GetByIdAsync(command.Id, false, false, token);

        column.Name = command.Name;

        await columnRepository.SaveChangesAsync(token);
    }
}
