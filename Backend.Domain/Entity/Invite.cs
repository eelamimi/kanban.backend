namespace Backend.Domain.Entity;

public class Invite : BaseEntity
{
    public Guid TeamId { get; set; }

    public Team Team { get; set; }

    public Guid RoleId { get; set; }

    public Role Role { get; set; }

    public string Token { get; set;  } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }
}
