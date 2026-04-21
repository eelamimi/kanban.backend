namespace Backend.Database.Configuration;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("Attachment");

        // PK
        builder.HasKey(a => a.Id);

        // Properties
        builder.Property(a => a.FileName)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(a => a.Content)
            .IsRequired()
            .HasColumnType("bytea");

        builder.Property(a => a.ContentType)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(a => a.Size)
            .IsRequired();

        // Relations one-to-one, one-to-many, many-to-one, many-to-many
        builder.HasOne(a => a.Commentary)
            .WithMany(c => c.Attachments)
            .HasForeignKey("CommentaryId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(a => a.Issue)
            .WithMany()
            .HasForeignKey(a => a.IssueId)
            .OnDelete(DeleteBehavior.Restrict);

        // IXs
        builder.HasIndex("CommentaryId")
            .HasDatabaseName("IX__Attachment__CommentaryId");
    }
}
