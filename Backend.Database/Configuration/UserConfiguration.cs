namespace Backend.Database.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        // PK
        builder.HasKey(u => u.Id);
        
        // Properties
        builder.Property(u => u.Email)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(256);
        
        builder.Property(u => u.Password)
            .IsRequired();

        // Relations one-to-one, one-to-many, many-to-one, many-to-many
        builder.HasOne(u => u.UserProfile)
            .WithOne(up => up.User)
            .HasForeignKey<User>("UserProfileId")
            .OnDelete(DeleteBehavior.Cascade);

        // IXs
        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("IX__User__Email");

        builder.HasIndex("UserProfileId")
            .IsUnique()
            .HasDatabaseName("IX__User__UserProfileId");
    }
}
