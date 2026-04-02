namespace Backend.Domain.Entity;

public class ColumnRelation
{
    public virtual Column? PrevColumn { get; set; }

    public Guid PrevColumnId { get; set; }

    public virtual Column? NextColumn { get; set; }

    public Guid NextColumnId { get; set; }
}
