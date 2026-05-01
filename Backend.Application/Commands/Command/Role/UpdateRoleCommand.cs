namespace Backend.Application.Commands.Command;

public class UpdateRoleCommand : ICommand<TeamDetailsResponse>
{
    public Guid UserProfileId { get; set; }

    public Guid RoleId { get; init; }

    public string Name { get; set; } = string.Empty;
}
