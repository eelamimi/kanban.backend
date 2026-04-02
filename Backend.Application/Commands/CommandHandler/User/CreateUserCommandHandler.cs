namespace Backend.Application.Commands.CommandHandler;

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IUserProfileRepository userProfileRepository,
    IPasswordHasher passwordHasher) 
    : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken token)
    {
        if (command.Password != command.ConfirmPassword)
            throw new UserInputException("Passwords are not equal");

        if (await userRepository.ExistsByEmailAsync(command.Email, token))
            throw new ConflictException("Email already exists");

        var hashedPassword = passwordHasher.HashPassword(command.Password);

        var userProfile = new UserProfile
        {
            FirstName = command.FirstName,
            SecondName = command.SecondName,
            CreatedAt = DateTime.UtcNow
        };
        var user = new User
        {
            Email = command.Email,
            Password = hashedPassword,
            UserProfile = userProfile,
        };

        userRepository.Add(user);
        userProfileRepository.Add(userProfile);
        await userRepository.SaveChangesAsync(token);

        return new CreateUserResult
        {
            UserId = user.Id,
            UserProfileId = userProfile.Id
        };
    }
}
