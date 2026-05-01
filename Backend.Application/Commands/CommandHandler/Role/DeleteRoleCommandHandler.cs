namespace Backend.Application.Commands.CommandHandler;

public class DeleteRoleCommandHandler(
    IRoleRepository roleRepository,
    ITeamRepository teamRepository,
    ITeamUserProfileRepository teamUserProfileRepository) :
    ICommandHandler<DeleteRoleCommand>
{
    public async Task Handle(DeleteRoleCommand command, CancellationToken token)
    {
        if (await teamUserProfileRepository.IsRoleUses(command.RoleId, token))
            throw new ForbiddenException("Роль используется");

        var role = await roleRepository.GetByIdAsync(command.RoleId, token);

        if (!await teamUserProfileRepository.IsInTeam(command.UserProfileId, role.TeamId, token))
            throw new ForbiddenException("Пользователь не является участником команды");

        var team = await teamRepository.GetByIdAsync(role.TeamId, true, token: token);
        if (team.Roles.Count < 2)
            throw new ForbiddenException("В команде должна быть минимум одна роль");

        roleRepository.Remove(role);
        await roleRepository.SaveChangesAsync(token);
    }
}
