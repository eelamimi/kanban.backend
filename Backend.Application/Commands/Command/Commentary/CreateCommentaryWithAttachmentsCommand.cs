namespace Backend.Application.Commands.Command;

public class CreateCommentaryWithAttachmentsCommand : ICommand<IssueResponse>
{
    public Guid IssueId { get; init; }

    public Guid AuthorId { get; init; }

    public string Content { get; init; } = string.Empty;

    public IEnumerable<IFormFile> Files { get; init; } = [];
}
