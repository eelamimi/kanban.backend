namespace Backend.Application.Commands.CommandHandler;

public class DeleteUserFromTeamCommandHandler(
    ITeamUserProfileRepository teamUserProfileRepository) :
    ICommandHandler<DeleteUserFromTeamCommand>
{
    public async Task Handle(DeleteUserFromTeamCommand command, CancellationToken token)
    {
        var tup = await teamUserProfileRepository
            .GetByUserProfileAndTeamIdAsync(command.UserProfileId, command.TeamId, token: token);

        teamUserProfileRepository.Remove(tup);
        await teamUserProfileRepository.SaveChangesAsync(token);
    }
}
