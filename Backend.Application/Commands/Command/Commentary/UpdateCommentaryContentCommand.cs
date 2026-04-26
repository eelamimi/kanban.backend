namespace Backend.Application.Commands.Command;

public class UpdateCommentaryContentCommand : ICommand<CommentaryResponse>
{
    public Guid Id { get; init; }

    public Guid UserProfileId { get; init; }

    public string Content { get; init; } = string.Empty;
}
