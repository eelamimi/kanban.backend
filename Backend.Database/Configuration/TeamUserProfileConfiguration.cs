namespace Backend.Database.Configuration;

public class TeamUserProfileConfiguration : IEntityTypeConfiguration<TeamUserProfile>
{
    public void Configure(EntityTypeBuilder<TeamUserProfile> builder)
    {
        builder.ToTable("TeamUserProfile");

        // PK
        builder.HasKey(["TeamId", "UserProfileId", "RoleId"]);

        // Relations one-to-one, one-to-many, many-to-one, many-to-many
        builder.HasOne(tup => tup.UserProfile)
            .WithMany(up => up.TeamUserProfiles)
            .HasForeignKey("UserProfileId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tup => tup.Team)
            .WithMany(t => t.TeamUserProfiles)
            .HasForeignKey("TeamId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tup => tup.Role)
            .WithMany(r => r.TeamUserProfiles)
            .HasForeignKey("RoleId")
            .OnDelete(DeleteBehavior.Restrict);

        // IXs
        builder.HasIndex("UserProfileId")
            .HasDatabaseName("IX__TeamUserProfile__UserProfileId");

        builder.HasIndex("TeamId")
            .HasDatabaseName("IX__TeamUserProfile__TeamId");

        builder.HasIndex("RoleId")
            .HasDatabaseName("IX__TeamUserProfile__RoleId");

        builder.HasIndex(["TeamId", "UserProfileId"])
            .IsUnique()
            .HasDatabaseName("IX__TeamUserProfile__TeamUserProfileUnique");
    }
}
