namespace Backend.Application.Commands.Command;

public class UpdateUserRoleCommand : ICommand<UserRolePair>
{
    public Guid UserProfileId { get; init; }

    public Guid RoleId { get; init; }
}
