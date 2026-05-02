namespace Backend.Application.Commands.Command;

public class AddUserToTeamCommand : ICommand<Guid>
{
    public Guid UserProfileId { get; set; }

    public string Token { get; init; } = string.Empty;
}
