namespace Backend.Database.Repository;

public class TeamRepository(ApplicationDbContext context) : ITeamRepository
{
    public async Task<Team> GetByIdAsync(Guid id, bool withRoles = false, bool withProjects = false, CancellationToken token = default)
    {
        return await TryGetByIdAsync(id, withRoles, withProjects, token)
            ?? throw new NullReferenceException("Team is null");
    }

    public async Task<Team?> TryGetByIdAsync(Guid id, bool withRoles = false, bool withProjects = false, CancellationToken token = default)
    {
        var query = context.Teams.AsQueryable();

        if (withProjects)
            query = query.Include(t => t.Projects);

        if (withRoles)
            query = query.Include(t => t.Roles);

        return await query
            .FirstOrDefaultAsync(t => t.Id == id, token);
    }

    public async Task<IEnumerable<Team>> GetAllAsync(CancellationToken token = default)
    {
        return await context.Teams.ToListAsync(token);
    }

    public void Add(Team team)
    {
        context.Teams.Add(team);
    }

    public void Update(Team team)
    {
        context.Teams.Update(team);
    }

    public void Remove(Team team)
    {
        context.Teams.Remove(team);
    }

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await context.SaveChangesAsync(token);
    }
}