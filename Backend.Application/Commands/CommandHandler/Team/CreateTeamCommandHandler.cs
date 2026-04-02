namespace Backend.Application.Commands.CommandHandler;

public class CreateTeamCommandHandler(
    IUserProfileRepository userProfileRepository,
    ITeamRepository teamRepository,
    IRoleRepository roleRepository,
    ITeamUserProfileRepository teamUserProfileRepository) : ICommandHandler<CreateTeamCommand, CreateTeamResult>
{
    public async Task<CreateTeamResult> Handle(CreateTeamCommand command, CancellationToken token)
    {
        var team = new Team
        {
            Name = command.TeamName
        };
        var role = new Role
        {
            Name = command.RoleName,
            Team = team,
        };
        var userProfile = await userProfileRepository.GetByIdAsync(command.UserProfileId, token);
        var teamUserProfile = new TeamUserProfile
        {
            Role = role,
            Team = team,
            UserProfile = userProfile,
        };

        teamUserProfileRepository.Add(teamUserProfile);
        teamRepository.Add(team);
        roleRepository.Add(role);
        await roleRepository.SaveChangesAsync(token);

        return new CreateTeamResult
        {
            TeamId = team.Id,
        };
    }
}
