namespace Backend.Database.Configuration;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("UserProfile");

        // PK
        builder.HasKey(up => up.Id);

        // Properties
        builder.Property(up => up.FirstName)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(256);

        builder.Property(up => up.SecondName)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(256);

        builder.Property(up => up.Extension)
            .IsRequired()
            .HasMaxLength(16);

        builder.Property(up => up.Avatar)
            .HasColumnType("bytea")
            .HasMaxLength(5 * 1024 * 1024);  // 5MB

        builder.Property(up => up.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        // Relations one-to-one, one-to-many, many-to-one, many-to-many
        builder.HasOne(up => up.User)
            .WithOne(u => u.UserProfile)
            .HasForeignKey<User>("UserProfileId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(up => up.TeamUserProfiles)
            .WithOne(tup => tup.UserProfile)
            .HasForeignKey("UserProfileId")
            .OnDelete(DeleteBehavior.Cascade);

        // IXs
        builder.HasIndex(up => new { up.FirstName, up.SecondName })
            .IsUnique()
            .HasDatabaseName("IX__UserProfile__FullName");
    }
}
