namespace Backend.Application.Queries.Query;

public class TeamsQuery : ICommand<IEnumerable<TeamResponse>>
{
    public Guid UserProfileId { get; init; }

    public Guid PersonUserId { get; init; }
}
