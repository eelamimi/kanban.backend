namespace Backend.Database.Repository;

public class TeamUserProfileRepository(ApplicationDbContext context) : ITeamUserProfileRepository
{
    public async Task<IEnumerable<TeamUserProfile>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.TeamUserProfiles.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TeamUserProfile>> GetTeamsByUserProfileIdAsync(Guid userProfileId, CancellationToken token = default)
    {
        return await context.TeamUserProfiles
            .Include(tup => tup.Team)
            .Include(tup => tup.Role)
            .Where(tup => tup.UserProfileId == userProfileId)
            .ToListAsync(token);
    }

    public async Task<IEnumerable<TeamUserProfile>> GetUsersByTeamIdAsync(Guid teamId, CancellationToken token = default)
    {
        return await context.TeamUserProfiles
            .Include(tup => tup.UserProfile)
            .Include(tup => tup.UserProfile.User)
            .Include(tup => tup.Team)
            .Include(tup => tup.Role)
            .Where(tup => tup.Team.Id == teamId)
            .ToListAsync(token);
    }

    public async Task<bool> IsSameTeam(Guid firstUserProfile, Guid secondUserProfile, CancellationToken token = default)
    {
        return await context.TeamUserProfiles
            .Where(tup => tup.UserProfileId == firstUserProfile || tup.UserProfileId == secondUserProfile)
            .GroupBy(tup => tup.TeamId)
            .AnyAsync(group => group.Select(tup => tup.UserProfileId)
                .Distinct()
                .Count() == 2, 
                token);
    }

    public async Task<bool> IsInTeam(Guid userProfileId, Guid teamId, CancellationToken token = default)
    {
        return await context.TeamUserProfiles
            .AnyAsync(tup => tup.TeamId == teamId && tup.UserProfileId == userProfileId, token);
    }

    public async Task<bool> IsInProject(Guid userProfileId, Guid projectId, CancellationToken token = default)
    {
        return await context.TeamUserProfiles
            .Where(tup => tup.UserProfileId == userProfileId)
            .SelectMany(tup => tup.Team.Projects)
            .AnyAsync(p => p.Id == projectId, token);
    }

    public void Add(TeamUserProfile teamUserProfile)
    {
        context.TeamUserProfiles.Add(teamUserProfile);
    }

    public void Update(TeamUserProfile teamUserProfile)
    {
        context.TeamUserProfiles.Update(teamUserProfile);
    }

    public void Remove(TeamUserProfile teamUserProfile)
    {
        context.TeamUserProfiles.Remove(teamUserProfile);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}