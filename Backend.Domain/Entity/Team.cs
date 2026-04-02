namespace Backend.Domain.Entity;

public class Team : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Role> Roles { get; set; } = new HashSet<Role>();

    public ICollection<Project> Projects { get; set; } = new HashSet<Project>();

    public ICollection<TeamUserProfile> TeamUserProfiles { get; set; } = new HashSet<TeamUserProfile>();
}
