namespace Backend.Application.Commands.CommandHandler;

public class CreateInviteCommandHandler(
    ITokenService tokenService,
    IInviteRepository inviteRepository):
    ICommandHandler<CreateInviteCommand, string>
{
    public async Task<string> Handle(CreateInviteCommand command, CancellationToken token)
    {
        var inviteToken = tokenService.GenerateToken();
        var createdAt = DateTime.UtcNow;

        var invite = new Invite
        {
            TeamId = command.TeamId,
            Token = inviteToken,
            CreatedAt = createdAt,
            ExpiresAt = createdAt.AddDays(1),
        };

        inviteRepository.Add(invite);
        await inviteRepository.SaveChangesAsync(token);

        return inviteToken;
    }
}
