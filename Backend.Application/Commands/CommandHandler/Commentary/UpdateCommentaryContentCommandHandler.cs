namespace Backend.Application.Commands.CommandHandler;

public class UpdateCommentaryContentCommandHandler(
    ICommentaryRepository commentaryRepository)
    : ICommandHandler<UpdateCommentaryContentCommand, CommentaryResponse>
{
    public async Task<CommentaryResponse> Handle(UpdateCommentaryContentCommand command, CancellationToken token)
    {
        var commentary = await commentaryRepository.GetByIdAsync(command.Id, token);

        if (commentary.AuthorId != command.UserProfileId)
            throw new ForbiddenException("Пользователь не является автором комментария");

        commentary.Content = command.Content;
        commentary.LastEditedAt = DateTime.UtcNow;

        commentaryRepository.Update(commentary);
        await commentaryRepository.SaveChangesAsync(token);

        return commentary.Map();
    }
}
