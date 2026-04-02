namespace Backend.Domain.Entity;

public class TeamUserProfile 
{
    public virtual UserProfile UserProfile { get; set; }

    public Guid UserProfileId { get; set; }

    public virtual Team Team { get; set; }

    public Guid TeamId { get; set; }

    public virtual Role Role { get; set; }

    public Guid RoleId { get; set; }
}
