namespace Backend.Application.Map;

public static class UserResponseMap
{
    public static UserResponse Map(this UserProfile userProfile)
    {
        return new UserResponse
        {
            Id = userProfile.Id,
            FirstName = userProfile.FirstName,
            SecondName = userProfile.SecondName,
            Email = userProfile.User.Email,
            Avatar = userProfile.Avatar,
            CreatedAt = userProfile.CreatedAt,
        };
    }

    public static UserResponse Map(this User user)
    {
        return new UserResponse
        {
            Id = user.UserProfile.Id,
            FirstName = user.UserProfile.FirstName,
            SecondName = user.UserProfile.SecondName,
            Email = user.Email,
            Avatar = user.UserProfile.Avatar,
            CreatedAt = user.UserProfile.CreatedAt,
        };
    }
}
