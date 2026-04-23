namespace Backend.Application.Commands.CommandHandler;

public class CreateAttachmentsCommandHandler(
    IMediator mediator,
    IAttachmentRepository attachmentRepository)
    : ICommandHandler<CreateAttachmentsCommand>
{
    public async Task Handle(CreateAttachmentsCommand command, CancellationToken token)
    {
        foreach (var file in command.Files)
        {
            await mediator.Send(new CreateAttachmentCommand
            {
                IssueId = command.IssueId,
                CommentaryId = command.CommentaryId,
                File = file,
                Save = false,
            }, token);
        }

        if (command.Save)
            await attachmentRepository.SaveChangesAsync(token);
    }
}
