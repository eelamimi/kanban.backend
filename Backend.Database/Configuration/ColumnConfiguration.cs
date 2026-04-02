namespace Backend.Database.Configuration;

public class ColumnConfiguration : IEntityTypeConfiguration<Column>
{
    public void Configure(EntityTypeBuilder<Column> builder)
    {
        builder.ToTable("Column");

        // PK
        builder.HasKey(c => c.Id);

        // Properties
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(16);

        builder.Property(c => c.Position)
            .IsRequired();

        // Relations one-to-one, one-to-many, many-to-one, many-to-many
        builder.HasOne(c => c.Project)
            .WithMany(p => p.Columns)
            .HasForeignKey("ProjectId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Issues)
            .WithOne(i => i.Column)
            .HasForeignKey("ColumnId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.NextColumnRelations)
            .WithOne(cr => cr.PrevColumn)
            .HasForeignKey("PrevColumnId")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.PrevColumnRelations)
            .WithOne(cr => cr.NextColumn)
            .HasForeignKey("NextColumnId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
