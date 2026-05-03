namespace Backend.Database.Repository;

public class TeamUserProfileRepository(ApplicationDbContext context) : ITeamUserProfileRepository
{
    public async Task<IEnumerable<TeamUserProfile>> GetAllAsync(CancellationToken token = default)
    {
        return await context.TeamUserProfiles.ToListAsync(token);
    }

    public async Task<IEnumerable<TeamUserProfile>> GetTeamsByUserProfileIdAsync(Guid userProfileId, CancellationToken token = default)
    {
        return await context.TeamUserProfiles
            .Include(tup => tup.Team)
                .ThenInclude(t => t.Projects)
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

    public async Task<TeamUserProfile> GetByUserProfileAndTeamIdAsync(
        Guid userProfileId,
        Guid teamId,
        bool withUserProfiles = false,
        bool withTeam = false,
        bool withRole = false,
        CancellationToken token = default)
    {
        var query = context.TeamUserProfiles.AsQueryable();

        if (withUserProfiles)
            query = query
                .Include(tup => tup.UserProfile)
                    .ThenInclude(up => up.User);

        if (withTeam)
            query = query.Include(tup => tup.Team);

        if (withRole)
            query = query.Include(tup => tup.Role);

        return await query.FirstAsync(tup =>
                tup.UserProfileId == userProfileId && tup.TeamId == teamId,
                token);
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

    public async Task<bool> IsRoleUses(Guid roleId, CancellationToken token = default)
    {
        return await context.TeamUserProfiles
            .AnyAsync(tup => tup.RoleId == roleId, token);
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

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await context.SaveChangesAsync(token);
    }
}