namespace Backend.Application.Commands.Command;

public class CreateTeamCommand : ICommand<CreateTeamResult>
{
    public Guid UserProfileId { get; init; }

    public string TeamName { get; init; } = string.Empty;

    public string RoleName { get; init; } = string.Empty;
}
