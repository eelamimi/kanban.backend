namespace Backend.Database.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");

        // PK
        builder.HasKey(r => r.Id);

        // Properties
        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(16);

        // Relations one-to-one, one-to-many, many-to-one, many-to-many
        builder.HasOne(r => r.Team)
            .WithMany(t => t.Roles)
            .HasForeignKey("TeamId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(r => r.TeamUserProfiles)
            .WithOne(tup => tup.Role)
            .HasForeignKey("RoleId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
