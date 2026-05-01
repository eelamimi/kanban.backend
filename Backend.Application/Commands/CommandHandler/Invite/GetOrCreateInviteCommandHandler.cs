namespace Backend.Application.Commands.CommandHandler;

public class GetOrCreateInviteCommandHandler(
    IMediator mediator) :
    ICommandHandler<GetOrCreateInviteCommand, string>
{
    public async Task<string> Handle(GetOrCreateInviteCommand command, CancellationToken token)
    {
        var inviteToken = await mediator.Send(new GetActiveInviteQuery
        {
            TeamId = command.TeamId,
            RoleId = command.RoleId,
        }, token);

        if (inviteToken == null)
            return await mediator.Send(new CreateInviteCommand
            {
                TeamId = command.TeamId,
                RoleId = command.RoleId,
            }, token);

        return inviteToken;
    }
}
