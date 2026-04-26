namespace Backend.Application.Commands.CommandHandler;

public class DeleteCommentaryCommandHandler(
    ICommentaryRepository commentaryRepository)
    : ICommandHandler<DeleteCommentaryCommand>
{
    public async Task Handle(DeleteCommentaryCommand command, CancellationToken token)
    {
        var commentary = await commentaryRepository.GetByIdAsync(command.Id, true, token);

        if (commentary.AuthorId != command.UserProfileId)
            throw new ForbiddenException("Пользователь не является автором комментария");

        commentaryRepository.Remove(commentary);
        await commentaryRepository.SaveChangesAsync(token);
    }
}
