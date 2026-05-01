namespace Backend.Application.Commands.Command;

public class UpdateUserRoleCommand : ICommand<UserRolePairResponse>
{
    public Guid UserProfileId { get; init; }

    public Guid RoleId { get; init; }
}
