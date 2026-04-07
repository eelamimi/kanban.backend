namespace Backend.Domain.Entity;

public class Commentary : BaseEntity
{
    public virtual UserProfile Author { get; set; }

    public Guid AuthorId { get; set; }

    public virtual Issue Issue { get; set; }

    public Guid IssueId { get; set; }

    public string Content { get; set; } = string.Empty;

    public bool IsDescription { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastEditedAt { get; set; } = null;

    public ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();
}
