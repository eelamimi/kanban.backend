namespace Backend.Application.Queries.QueryHandler;

public class UserQueryHandler(
    ITeamUserProfileRepository teamUserProfileRepository,
    IUserProfileRepository userProfileRepository)
    : ICommandHandler<UserQuery, UserResponse>
{
    public async Task<UserResponse> Handle(UserQuery query, CancellationToken token)
    {
        if (query.UserId == query.PersonUserId || 
            await teamUserProfileRepository.IsSameTeam(query.UserId, query.PersonUserId, token))
        {
            var userProfile = await userProfileRepository.GetByIdAsync(query.UserId, token);
            return userProfile.Map();
        }

        throw new ForbiddenException("Different teams");
    }
}
