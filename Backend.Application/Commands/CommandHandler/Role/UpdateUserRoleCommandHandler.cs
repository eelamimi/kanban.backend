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

        teamUserProfileRepository.Remove(teamUserProfile);
        await teamUserProfileRepository.SaveChangesAsync(token);

        var newTeamUserProfile = new TeamUserProfile
        {
            RoleId = newRole.Id,
            TeamId = newRole.TeamId,
            UserProfileId = command.UserProfileId,
        };

        teamUserProfileRepository.Add(newTeamUserProfile);
        await teamUserProfileRepository.SaveChangesAsync(token);

        return newTeamUserProfile.Map<UserRolePairResponse>();
    }
}
