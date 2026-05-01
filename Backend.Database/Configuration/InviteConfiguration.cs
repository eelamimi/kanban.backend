namespace Backend.Database.Configuration;

public class InviteConfiguration : IEntityTypeConfiguration<Invite>
{
    public void Configure(EntityTypeBuilder<Invite> builder)
    {
        builder.ToTable("Invite");

        // PK
        builder.HasKey(i => i.Id);

        // Properties
        builder.Property(i => i.Token)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(i => i.CreatedAt)
            .IsRequired();

        builder.Property(i => i.ExpiresAt)
            .IsRequired();

        // Relations one-to-one, one-to-many, many-to-one, many-to-many
        builder.HasOne(i => i.Team)
            .WithMany()
            .HasForeignKey(i => i.TeamId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(i => i.Role)
            .WithMany()
            .HasForeignKey(i => i.RoleId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // IXs
        builder.HasIndex(i => i.Token)
            .IsUnique()
            .HasDatabaseName("IX__Invite__Token");

        builder.HasIndex(i => i.TeamId)
            .HasDatabaseName("IX__Invite__TeamId");

        builder.HasIndex(i => i.ExpiresAt)
            .HasDatabaseName("IX__Invite__ExpiresAt");
    }
}
