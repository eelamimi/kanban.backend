namespace Backend.Application.Commands.Command;

public class CreateInviteCommand : ICommand<string>
{
    public Guid TeamId { get; init; }
}
