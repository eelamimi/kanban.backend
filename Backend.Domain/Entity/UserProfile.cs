namespace Backend.Domain.Entity;

public class UserProfile : BaseEntity
{
    public virtual User User { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string SecondName { get; set; } = string.Empty;

    public string Extension { get; set; } = string.Empty;

    public byte[] Avatar { get; set; } = [];

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Project> Projects { get; set; } = new HashSet<Project>();

    public ICollection<TeamUserProfile> TeamUserProfiles { get; set; } = new HashSet<TeamUserProfile>();
}
