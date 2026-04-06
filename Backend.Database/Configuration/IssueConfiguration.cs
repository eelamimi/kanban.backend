namespace Backend.Database.Configuration;

public class IssueConfiguration : IEntityTypeConfiguration<Issue>
{
    public void Configure(EntityTypeBuilder<Issue> builder)
    {
        builder.ToTable("Issue");

        // PK
        builder.HasKey(i => i.Id);

        // Properties
        builder.Property(i => i.NumberInProject)
            .IsRequired();

        builder.Property(i => i.Priority)
            .IsRequired();

        builder.Property(i => i.StoryPoints)
            .IsRequired();

        builder.Property(i => i.Title)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(i => i.IssueType)
            .IsRequired();
        
        builder.Property(i => i.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(i => i.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(i => i.ClosedAt)
            .IsRequired(false);

        // Relations one-to-one, one-to-many, many-to-one, many-to-many
        builder.HasOne(i => i.Assignee)
            .WithMany()
            .HasForeignKey(i => i.AssigneeId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(i => i.Author)
            .WithMany()
            .HasForeignKey(i => i.AuthorId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(i => i.Column)
            .WithMany(c => c.Issues)
            .HasForeignKey(i => i.ColumnId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(i => i.Project)
            .WithMany()
            .HasForeignKey(i => i.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(i => i.Commentaries)
            .WithOne(c => c.Issue)
            .HasForeignKey(c => c.IssueId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // IXs
        builder.HasIndex("AssigneeId")
            .IsUnique()
            .HasDatabaseName("IX__Issue__AssigneeId");

        builder.HasIndex("AuthorId")
            .IsUnique()
            .HasDatabaseName("IX__Issue__AuthorId");

        builder.HasIndex(i => new { i.ProjectId, i.NumberInProject, i.IsDeleted })
            .IsUnique()
            .HasFilter("\"IsDeleted\" = false");

        builder.HasQueryFilter(i => !i.IsDeleted);
    }
}
