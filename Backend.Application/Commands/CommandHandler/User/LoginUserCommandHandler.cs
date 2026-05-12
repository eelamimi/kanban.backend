namespace Backend.Application.Commands.CommandHandler;

public class LoginUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtService jwtService) 
    : ICommandHandler<LoginUserCommand, RegistryOrLoginUserResponse>
{
    public async Task<RegistryOrLoginUserResponse> Handle(LoginUserCommand command, CancellationToken token)
    {
        var user = await userRepository.TryGetByEmailAsync(command.Email, true, token)
            ?? throw new UserInputException($"Пользователя с почтой {command.Email} не существует");

        if (!passwordHasher.VerifyPassword(command.Password, user.Password))
            throw new UserInputException("Неверный пароль, попробуйте ещё раз");

        var userProfile = user.UserProfile;
        var jwtToken = jwtService.GenerateToken(user.Id, userProfile.FirstName, userProfile.SecondName, user.Email);

        return new RegistryOrLoginUserResponse
        {
            UserId = user.Id,
            UserProfileId = user.UserProfile.Id,
            Token = jwtToken
        };
    }
}
