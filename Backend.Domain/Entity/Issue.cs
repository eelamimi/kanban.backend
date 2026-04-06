namespace Backend.Domain.Entity;

public class Issue : BaseEntity
{
    public virtual Project Project { get; set; }

    public Guid ProjectId { get; set; }
    
    public virtual Column Column { get; set; }

    public Guid ColumnId { get; set; }

    public virtual UserProfile Assignee { get; set; }

    public Guid AssigneeId { get; set; }

    public virtual UserProfile Author { get; set; }

    public Guid AuthorId { get; set; }

    public int NumberInProject { get; set; }

    public string Title { get; set; } = string.Empty;

    public IssueType IssueType { get; set; }

    public int StoryPoints { get; set; }

    public IssuePriority IssuePriority { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ClosedAt { get; set; } = null;

    public ICollection<Commentary> Commentaries { get; set; } = new HashSet<Commentary>();
}
