namespace Backend.Database.Configuration;

public class IssueConfiguration : IEntityTypeConfiguration<Issue>
{
    public void Configure(EntityTypeBuilder<Issue> builder)
    {
        builder.ToTable("Issue");

        // PK
        builder.HasKey(i => i.Id);

        // Properties
        builder.Property(i => i.PublicId)
            .IsRequired()
            .HasMaxLength(16);

        builder.Property(i => i.Priority)
            .IsRequired();

        builder.Property(i => i.StoryPoints)
            .IsRequired();

        builder.Property(i => i.Title)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(i => i.IssueType)
            .IsRequired();

        builder.Property(i => i.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(i => i.ClosedAt)
            .IsRequired(false);

        // Relations one-to-one, one-to-many, many-to-one, many-to-many
        builder.HasOne(i => i.Assignee)
            .WithMany()
            .HasForeignKey("AssigneeId")
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(i => i.Author)
            .WithMany()
            .HasForeignKey("AuthorId")
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(i => i.Column)
            .WithMany(c => c.Issues)
            .HasForeignKey("ColumnId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(i => i.Commentaries)
            .WithOne(c => c.Issue)
            .HasForeignKey("IssueId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // IXs
        builder.HasIndex("AssigneeId")
            .IsUnique()
            .HasDatabaseName("IX__Issue__AssigneeId");

        builder.HasIndex("AuthorId")
            .IsUnique()
            .HasDatabaseName("IX__Issue__AuthorId");
    }
}
