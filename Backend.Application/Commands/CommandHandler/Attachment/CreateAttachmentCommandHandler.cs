namespace Backend.Application.Commands.CommandHandler;

public class CreateAttachmentCommandHandler(
    IAttachmentRepository attachmentRepository)
    : ICommandHandler<CreateAttachmentCommand>
{
    public async Task Handle(CreateAttachmentCommand command, CancellationToken token)
    {
        var file = command.File;
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream, token);
        var fileBytes = memoryStream.ToArray();

        var attachment = new Attachment
        {
            IssueId = command.IssueId,
            CommentaryId = command.CommentaryId,
            Content = fileBytes,
            ContentType = file.ContentType,
            FileName = file.FileName,
            Size = file.Length
        };

        attachmentRepository.Add(attachment);

        if (command.Save)
            await attachmentRepository.SaveChangesAsync(token);
    }
}
