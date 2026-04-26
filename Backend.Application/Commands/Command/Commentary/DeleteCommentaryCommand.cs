namespace Backend.Application.Commands.Command;

public class DeleteCommentaryCommand : ICommand
{
    public Guid Id { get; init; }

    public Guid UserProfileId { get; init; }
}
