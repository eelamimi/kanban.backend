namespace Backend.Database.Repository;

public class ProjectRepository(ApplicationDbContext context) : IProjectRepository
{
    public async Task<Project> GetByIdAsync(Guid id, bool includeIssues = false, bool includeTeam = false, CancellationToken cancellationToken = default)
    {
        return await TryGetByIdAsync(id, includeIssues, includeTeam, cancellationToken)
            ?? throw new InvalidOperationException($"Project with id {id} was not found.");
    }

    public async Task<Project?> TryGetByIdAsync(Guid id, bool includeIssues = false, bool includeTeam = false, CancellationToken cancellationToken = default)
    {
        var query = context.Projects.AsQueryable();

        if (includeIssues)
            query = query
                .Include(p => p.Columns)
                    .ThenInclude(c => c.Issues)
                        .ThenInclude(i => i.Assignee)
                .Include(p => p.Columns)
                    .ThenInclude(c => c.Issues)
                        .ThenInclude(i => i.Author);

        if (includeTeam)
            query = query.Include(p => p.Team)
                .ThenInclude(t => t.TeamUserProfiles)
                    .ThenInclude(tup => tup.UserProfile);

        return await query
            .Include(p => p.Creator)
                .ThenInclude(up => up.User)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Project>> GetAllByTeamIdAsync(Guid teamId, CancellationToken token = default)
    {
        return await context.Projects
            .Include(p => p.Creator)
            .Include(p => p.Creator.User)
            .Include(p => p.Team)
            .Where(p => p.Team.Id == teamId)
            .ToListAsync(token);
    }

    public async Task<bool> HasColumnName(Guid id, string columnName, CancellationToken token = default)
    {
        return await context.Projects
            .Where(p => p.Id == id)
            .Select(p => p.Columns.Any(c => c.Name == columnName))
            .FirstOrDefaultAsync(token);
    }


    public async Task<IEnumerable<Project>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Projects.ToListAsync(cancellationToken);
    }

    public void Add(Project project)
    {
        context.Projects.Add(project);
    }

    public void Update(Project project)
    {
        context.Projects.Update(project);
    }

    public void Remove(Project project)
    {
        context.Projects.Remove(project);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}