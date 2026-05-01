namespace Backend.Application.Commands.CommandHandler;

public class GetInviteCommandHandler(
    IInviteRepository inviteRepository) :
    ICommandHandler<GetInviteCommand, string?>
{
    public async Task<string?> Handle(GetInviteCommand command, CancellationToken token)
    {
        return await inviteRepository.GetLastByTeamIdAsync(command.TeamId, token);
    }
}
