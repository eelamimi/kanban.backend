namespace Backend.Application.Commands.CommandHandler;

public class AddUserToTeamCommandHandler(
    ITokenService tokenService,
    IInviteRepository inviteRepository,
    ITeamUserProfileRepository teamUserProfileRepository) :
    ICommandHandler<AddUserToTeamCommand, bool>
{
    public async Task<bool> Handle(AddUserToTeamCommand command, CancellationToken token)
    {
        if (!tokenService.VerifyToken(command.Token))
            return false;

        var invite = await inviteRepository.GeByTokenAsync(command.Token, token);
        if (invite == null || invite.ExpiresAt > DateTime.UtcNow)
            return false;

        teamUserProfileRepository.Add(new TeamUserProfile
        {
            RoleId = invite.RoleId,
            TeamId = invite.TeamId,
            UserProfileId = command.UserProfileId,
        });
        await teamUserProfileRepository.SaveChangesAsync(token);

        return true;
    }
}
