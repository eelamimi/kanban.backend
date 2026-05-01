namespace Backend.Application.Commands.Command;

public class DeleteRoleCommand : ICommand
{
    public Guid UserProfileId { get; set; }

    public Guid RoleId { get; init; }
}
