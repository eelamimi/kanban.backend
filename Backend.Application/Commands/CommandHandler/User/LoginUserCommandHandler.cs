namespace Backend.Application.Commands.CommandHandler;

public class LoginUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtService jwtService) 
    : ICommandHandler<LoginUserCommand, LoginUserResult>
{
    public async Task<LoginUserResult> Handle(LoginUserCommand command, CancellationToken token)
    {
        var user = await userRepository.TryGetByEmailAsync(command.Email, true, token)
            ?? throw new UserInputException($"User with email {command.Email} does not exist");

        if (!passwordHasher.VerifyPassword(command.Password, user.Password))
            throw new UserInputException("Incorrect password");

        var userProfile = user.UserProfile;
        var jwtToken = jwtService.GenerateToken(user.Id, userProfile.FirstName, userProfile.SecondName, user.Email);

        return new LoginUserResult
        {
            UserId = user.Id,
            UserProfileId = user.UserProfile.Id,
            Token = jwtToken
        };
    }
}
