namespace Backend.Database.Configuration;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("Team");

        // PK
        builder.HasKey(t => t.Id);

        // Properties
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(16);

        // Relations one-to-one, one-to-many, many-to-one, many-to-many
        builder.HasMany(t => t.Roles)
            .WithOne(r => r.Team)
            .HasForeignKey("TeamId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Projects)
            .WithOne(p => p.Team)
            .HasForeignKey("TeamId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.TeamUserProfiles)
            .WithOne(tup => tup.Team)
            .HasForeignKey("TeamId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
