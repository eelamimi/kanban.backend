namespace Backend.Application.Commands.CommandHandler;

public class UpdateUserRoleCommandHandler(
    IRoleRepository roleRepository,
    ITeamUserProfileRepository teamUserProfileRepository) :
    ICommandHandler<UpdateUserRoleCommand, UserRolePairResponse>
{
    public async Task<UserRolePairResponse> Handle(UpdateUserRoleCommand command, CancellationToken token)
    {
        var newRole = await roleRepository.GetByIdAsync(command.RoleId, token);

        if (!await teamUserProfileRepository.IsInTeam(command.UserProfileId, newRole.TeamId, token))
            throw new ForbiddenException("Пользователь не является участником команды");

        var teamUserProfile = await teamUserProfileRepository.GetByUserProfileAndTeamIdAsync(
            command.UserProfileId, newRole.TeamId, true, false, false, token);

        teamUserProfile.Role = newRole;
        teamUserProfile.RoleId = newRole.Id;

        teamUserProfileRepository.Update(teamUserProfile);
        await teamUserProfileRepository.SaveChangesAsync(token);

        return teamUserProfile.Map<UserRolePairResponse>();
    }
}
