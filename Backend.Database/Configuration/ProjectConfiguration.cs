namespace Backend.Database.Configuration;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Project");

        // PK
        builder.HasKey(p => p.Id);

        // Properties
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(16);

        builder.Property(p => p.ShortName)
            .IsRequired()
            .HasMaxLength(8);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(256);

        // Relations one-to-one, one-to-many, many-to-one, many-to-many
        builder.HasOne(p => p.Team)
            .WithMany(t => t.Projects)
            .HasForeignKey("TeamId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Creator)
            .WithMany(up => up.Projects)
            .HasForeignKey("CreatorId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Columns)
            .WithOne(c => c.Project)
            .HasForeignKey("ProjectId")
            .OnDelete(DeleteBehavior.Cascade);

        // IXs
        builder.HasIndex("TeamId")
            .IsUnique()
            .HasDatabaseName("IX__Project__TeamId");

        builder.HasIndex("CreatorId")
            .IsUnique()
            .HasDatabaseName("IX__Project__CreatorId");
    }
}
