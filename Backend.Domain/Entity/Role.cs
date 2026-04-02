namespace Backend.Domain.Entity;

public class Role : BaseEntity
{
    public virtual Team? Team { get; set; }

    public Guid TeamId { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<TeamUserProfile> TeamUserProfiles { get; set; } = new HashSet<TeamUserProfile>();
}
