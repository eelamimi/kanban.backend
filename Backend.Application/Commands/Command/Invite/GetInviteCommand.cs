namespace Backend.Application.Commands.Command;

public class GetInviteCommand : ICommand<string?>
{
    public Guid TeamId { get; init; }
}
