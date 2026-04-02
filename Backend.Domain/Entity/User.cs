namespace Backend.Domain.Entity;

public class User : BaseEntity
{
    public virtual UserProfile UserProfile { get; set; }

    public Guid UserProfileId { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
