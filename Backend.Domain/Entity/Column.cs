namespace Backend.Domain.Entity;

public class Column : BaseEntity
{
    public virtual Project Project { get; set; }

    public Guid ProjectId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Position { get; set; }

    public ICollection<Issue> Issues { get; set; } = new HashSet<Issue>();

    [InverseProperty(nameof(ColumnRelation.PrevColumn))]
    public virtual ICollection<ColumnRelation> NextColumnRelations { get; set; } = new HashSet<ColumnRelation>();

    [InverseProperty(nameof(ColumnRelation.NextColumn))]
    public virtual ICollection<ColumnRelation> PrevColumnRelations { get; set; } = new HashSet<ColumnRelation>();

    [NotMapped]
    public IEnumerable<Column> NextColumns => NextColumnRelations.Select(r => r.NextColumn);

    [NotMapped]
    public IEnumerable<Column> PrevColumns => PrevColumnRelations.Select(r => r.PrevColumn);
}
