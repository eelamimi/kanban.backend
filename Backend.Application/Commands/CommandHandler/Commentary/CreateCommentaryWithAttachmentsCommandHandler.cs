namespace Backend.Application.Commands.CommandHandler;

public class CreateCommentaryWithAttachmentsCommandHandler(
    IMediator mediator,
    ICommentaryRepository commentaryRepository)
    : ICommandHandler<CreateCommentaryWithAttachmentsCommand, IssueResponse>
{
    public async Task<IssueResponse> Handle(CreateCommentaryWithAttachmentsCommand command, CancellationToken token)
    {
        var commentary = new Commentary
        {
            IssueId = command.IssueId,
            AuthorId = command.AuthorId,
            Content = command.Content,
            CreatedAt = DateTime.UtcNow,
        };

        commentaryRepository.Add(commentary);

        await mediator.Send(new CreateAttachmentsCommand
        {
            IssueId = command.IssueId,
            CommentaryId = commentary.Id,
            Files = command.Files,
            Save = true,
        }, token);

        return await mediator.Send(new IssueDeatilsQuery
        {
            IssueId = command.IssueId,
        }, token);
    }
}
