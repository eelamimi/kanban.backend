namespace Backend.Application.Commands.Command;

public class CreateAttachmentsCommand : ICommand
{
    public Guid IssueId { get; init; }

    public Guid CommentaryId { get; init; }

    public bool Save { get; init; } = true;

    public IEnumerable<IFormFile> Files { get; init; } = [];
}
