namespace Backend.Application.Queries.QueryHandler;

public class GetActiveInviteQueryHandler(
    IInviteRepository inviteRepository) :
    ICommandHandler<GetActiveInviteQuery, string?>
{
    public async Task<string?> Handle(GetActiveInviteQuery command, CancellationToken token)
    {
        return await inviteRepository.GetLastByTeamIdAsync(command.TeamId, token);
    }
}
