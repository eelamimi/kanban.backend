namespace Backend.Database.Configuration;

public class ColumnRelationConfiguration : IEntityTypeConfiguration<ColumnRelation>
{
    public void Configure(EntityTypeBuilder<ColumnRelation> builder)
    {
        builder.ToTable("ColumnRelation");

        // PK
        builder.HasKey(["PrevColumnId", "NextColumnId"]);

        // Relations one-to-one, one-to-many, many-to-one, many-to-many
        builder.HasOne(cr => cr.PrevColumn)
            .WithMany(c => c.NextColumnRelations)
            .HasForeignKey("PrevColumnId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
        
        builder.HasOne(cr => cr.NextColumn)
            .WithMany(c => c.PrevColumnRelations)
            .HasForeignKey("NextColumnId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        // IXs
        builder.HasIndex(["PrevColumnId", "NextColumnId"])
            .IsUnique()
            .HasDatabaseName("IX__ColumnRelation__PrevNextColumns");
    }
}
