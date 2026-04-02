namespace Backend.Application.Queries.Query;

public class UserQuery : ICommand<UserResponse>
{
    public Guid UserId { get; init; }

    public Guid PersonUserId { get; init; }
}
