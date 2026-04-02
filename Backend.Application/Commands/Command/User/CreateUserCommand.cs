namespace Backend.Application.Commands.Command;

public class CreateUserCommand : ICommand<CreateUserResult>
{
    public string FirstName { get; init; } = string.Empty;

    public string SecondName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string ConfirmPassword { get; init; } = string.Empty;
}
