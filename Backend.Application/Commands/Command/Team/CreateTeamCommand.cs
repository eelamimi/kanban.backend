namespace Backend.Application.Commands.Command;

public class CreateTeamCommand : ICommand<TeamResponse>
{
    public Guid UserProfileId { get; set; }

    public string Name { get; init; } = string.Empty;

    public string RoleName { get; init; } = "Администратор";
}
