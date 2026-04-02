namespace Backend.Domain.Entity;

public class Project : BaseEntity
{
    public virtual UserProfile Creator { get; set; }

    public Guid CreatorId { get; set; }

    public virtual Team Team { get; set; }

    public Guid TeamId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ShortName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public ICollection<Column> Columns { get; set; } = new HashSet<Column>();
}
