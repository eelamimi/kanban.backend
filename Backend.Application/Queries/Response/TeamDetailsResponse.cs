namespace Backend.Application.Queries.Response;

public class TeamDetailsResponse
{
    public string Name { get; set; }

    public IEnumerable<ProjectResponse> Projects { get; set; }

    public IEnumerable<UserRolePair> UserRolePairs { get; set; }
}
