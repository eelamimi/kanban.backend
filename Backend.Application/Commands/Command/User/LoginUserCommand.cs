namespace Backend.Application.Commands.Command;

public class LoginUserCommand : ICommand<LoginUserResult>
{
    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;
}
