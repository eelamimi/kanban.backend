namespace Backend.Application.Commands.CommandHandler;

public class CreateRoleCommandHandler(
    IRoleRepository roleRepository,
    ITeamUserProfileRepository teamUserProfileRepository) :
    ICommandHandler<CreateRoleCommand, RoleResponse>
{
    public async Task<RoleResponse> Handle(CreateRoleCommand command, CancellationToken token)
    {
        if (!await teamUserProfileRepository.IsInTeam(command.UserProfileId, command.TeamId, token))
            throw new ForbiddenException("Пользователь не является участником команды");

        var role = new Role
        {
            TeamId = command.TeamId,
            Name = command.Name,
        };

        roleRepository.Add(role);
        await roleRepository.SaveChangesAsync(token);

        return role.Map();
    }
}
