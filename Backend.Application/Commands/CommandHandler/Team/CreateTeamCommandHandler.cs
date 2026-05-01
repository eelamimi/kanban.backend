namespace Backend.Application.Commands.CommandHandler;

public class CreateTeamCommandHandler(
    IUserProfileRepository userProfileRepository,
    ITeamRepository teamRepository,
    IRoleRepository roleRepository,
    ITeamUserProfileRepository teamUserProfileRepository) : ICommandHandler<CreateTeamCommand, TeamResponse>
{
    public async Task<TeamResponse> Handle(CreateTeamCommand command, CancellationToken token)
    {
        var team = new Team
        {
            Name = command.Name
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
        roleRepository.Add(new Role
        {
            Name = "Участник",
            Team = team
        });
        await roleRepository.SaveChangesAsync(token);

        return teamUserProfile.Map();
    }
}
