namespace Backend.Domain.Entity;

public class Issue : BaseEntity
{
    public virtual Column Column { get; set; }

    public Guid ColumnId { get; set; }

    public virtual UserProfile Assignee { get; set; }

    public Guid AssigneeId { get; set; }

    public virtual UserProfile Author { get; set; }

    public Guid AuthorId { get; set; }

    public string PublicId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public IssueType IssueType { get; set; }

    public int StoryPoints { get; set; }

    public IssuePriority Priority { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ClosedAt { get; set; } = null;

    public ICollection<Commentary> Commentaries { get; set; } = new HashSet<Commentary>();
}
