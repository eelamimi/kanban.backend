namespace Backend.Application.Commands.CommandHandler;

public class AddUserToTeamCommandHandler(
    ITokenService tokenService,
    IInviteRepository inviteRepository,
    ITeamUserProfileRepository teamUserProfileRepository) :
    ICommandHandler<AddUserToTeamCommand, Guid>
{
    public async Task<Guid> Handle(AddUserToTeamCommand command, CancellationToken token)
    {
        if (!tokenService.VerifyToken(command.Token))
            throw new InvalidOperationException("Приглашение недействительно");

        var invite = await inviteRepository.GeByTokenAsync(command.Token, token);
        if (invite == null || invite.ExpiresAt < DateTime.UtcNow)
            throw new InvalidOperationException("Приглашение недействительно");

        if (await teamUserProfileRepository.IsInTeam(command.UserProfileId, invite.TeamId, token))
            return invite.TeamId;

        teamUserProfileRepository.Add(new TeamUserProfile
        {
            RoleId = invite.RoleId,
            TeamId = invite.TeamId,
            UserProfileId = command.UserProfileId,
        });
        await teamUserProfileRepository.SaveChangesAsync(token);

        return invite.TeamId;
    }
}
