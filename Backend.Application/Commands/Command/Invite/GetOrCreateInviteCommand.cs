namespace Backend.Application.Commands.Command;

public class GetOrCreateInviteCommand : ICommand<string>
{
    public Guid TeamId { get; init; }

    public Guid RoleId { get; init; }
}
