namespace Backend.Application.Commands.Command;

public class CreateAttachmentCommand : ICommand
{
    public Guid IssueId { get; init; }

    public Guid CommentaryId { get; init; }

    public bool Save { get; init; } = true;

    public IFormFile File { get; init; }
}
