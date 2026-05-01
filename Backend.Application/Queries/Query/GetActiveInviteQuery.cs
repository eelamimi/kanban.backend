namespace Backend.Application.Queries.Query;

public class GetActiveInviteQuery : ICommand<string?>
{
    public Guid TeamId { get; init; }
}
