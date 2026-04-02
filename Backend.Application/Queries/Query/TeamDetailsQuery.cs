namespace Backend.Application.Queries.Query;

public class TeamDetailsQuery : ICommand<TeamDetailsResponse>
{
    public Guid UserProfileId { get; init; }

    public Guid TeamId { get; init; }
}
