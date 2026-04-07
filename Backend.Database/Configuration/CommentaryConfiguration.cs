namespace Backend.Database.Configuration;

public class CommentaryConfiguration : IEntityTypeConfiguration<Commentary>
{
    public void Configure(EntityTypeBuilder<Commentary> builder)
    {
        builder.ToTable("Commentary");

        // PK
        builder.HasKey(c => c.Id);

        // Properties
        builder.Property(c => c.Content)
            .IsRequired();

        builder.Property(c => c.IsDescription)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(c => c.LastEditedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        // Relations one-to-one, one-to-many, many-to-one, many-to-many
        builder.HasOne(c => c.Author)
            .WithMany()
            .HasForeignKey("AuthorId")
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(c => c.Issue)
            .WithMany(i => i.Commentaries)
            .HasForeignKey("IssueId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(c => c.Attachments)
            .WithOne(a => a.Commentary)
            .HasForeignKey("CommentaryId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // IXs
        builder.HasIndex("IssueId")
            .HasDatabaseName("IX__Commentary__IssueId");

        builder.HasIndex("AuthorId")
            .HasDatabaseName("IX__Commentary__AuthorId");
    }
}
