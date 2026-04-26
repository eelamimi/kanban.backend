namespace Backend.Application.Commands.Command;

public class UpdateCommentaryContentCommand : ICommand<CommentaryResponse>
{
    public Guid Id { get; init; }

    public Guid UserProfileId { get; set; }

    public string Content { get; init; } = string.Empty;
}
