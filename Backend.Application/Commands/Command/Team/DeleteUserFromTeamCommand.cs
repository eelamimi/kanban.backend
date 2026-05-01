namespace Backend.Application.Commands.Command;

public class DeleteUserFromTeamCommand : ICommand
{
    public Guid UserProfileId { get; init; }

    public Guid TeamId { get; init; }
}
