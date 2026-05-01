namespace Backend.Application.Commands.CommandHandler;

public class UpdateRoleCommandHandler(
    IMediator mediator,
    IRoleRepository roleRepository,
    ITeamUserProfileRepository teamUserProfileRepository) :
    ICommandHandler<UpdateRoleCommand, TeamDetailsResponse>
{
    public async Task<TeamDetailsResponse> Handle(UpdateRoleCommand command, CancellationToken token)
    {
        var role = await roleRepository.GetByIdAsync(command.RoleId, token);

        if (!await teamUserProfileRepository.IsInTeam(command.UserProfileId, role.TeamId, token))
            throw new ForbiddenException("Пользователь не является участником команды");

        role.Name = command.Name;
        roleRepository.Update(role);
        await roleRepository.SaveChangesAsync(token);

        return await mediator.Send(new TeamDetailsQuery
        {
            UserProfileId = command.UserProfileId,
            TeamId = role.TeamId,
        }, token);
    }
}
