namespace Backend.Application.Commands.CommandHandler;

public class UpdateTeamNameCommandHandler(
    ITeamRepository teamRepository,
    ITeamUserProfileRepository teamUserProfileRepository) :
    ICommandHandler<UpdateTeamNameCommand>
{
    public async Task Handle(UpdateTeamNameCommand command, CancellationToken token)
    {
        if (!await teamUserProfileRepository.IsInTeam(command.UserProfileId, command.TeamId, token))
            throw new ForbiddenException("Пользователь не является участником команды");

        var team = await teamRepository.GetByIdAsync(command.TeamId, token: token);
        team.Name = command.Name;

        teamRepository.Update(team);
        await teamRepository.SaveChangesAsync(token);
    }
}
