namespace Backend.Application.Commands.CommandHandler;

public class RegisterUserCommandHandler(
    IMediator mediator, 
    IJwtService jwtService) 
    : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand command, CancellationToken token)
    {
        var createUserResult = await mediator.Send(new CreateUserCommand
        {
            FirstName = command.FirstName,
            SecondName = command.SecondName,
            Email = command.Email,
            Password = command.Password,
            ConfirmPassword = command.ConfirmPassword
        }, token);
        var userProfileId = createUserResult.UserProfileId;
        var userId = createUserResult.UserId;

        var createTeamResult = await mediator.Send(new CreateTeamCommand
        {
            UserProfileId = userProfileId,
            TeamName = "Моя команда №1",
            RoleName = "Администратор"
        }, token);
        var teamId = createTeamResult.TeamId;

        await mediator.Send(new CreateProjectCommand
        {
            TeamId = teamId,
            UserProfileId = userProfileId,
            Name = "Мой проект №1",
            ShortName = "МП",
            Description = "Это мой первый проект! Так увлекательно!"
        }, token);

        var jwtToken = jwtService.GenerateToken(userId, command.FirstName, command.SecondName, command.Email);

        return new RegisterUserResult
        {
            UserId = userId,
            UserProfileId = userProfileId,
            Token = jwtToken
        };
    }
}
