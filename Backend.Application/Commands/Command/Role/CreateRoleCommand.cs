namespace Backend.Application.Commands.Command;

public class CreateRoleCommand : ICommand<RoleResponse>
{
    public Guid UserProfileId { get; set; }

    public Guid TeamId { get; init; }

    public string Name { get; init; } = string.Empty;
}
