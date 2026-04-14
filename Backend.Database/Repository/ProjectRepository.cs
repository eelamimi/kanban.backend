using Microsoft.EntityFrameworkCore.Query;

namespace Backend.Database.Repository;

public class ProjectRepository(ApplicationDbContext context) : IProjectRepository
{
    public async Task<Project> GetByIdAsync(Guid id, bool includeColumns = false, bool includeIssues = false, bool includeTeam = false, CancellationToken token = default)
    {
        return await TryGetByIdAsync(id, includeColumns, includeIssues, includeTeam, token)
            ?? throw new InvalidOperationException($"Project with id {id} was not found.");
    }

    public async Task<Project?> TryGetByIdAsync(Guid id, bool includeColumns = false, bool includeIssues = false, bool includeTeam = false, CancellationToken token = default)
    {
        var query = context.Projects.AsQueryable();
            
        if (includeColumns)
            query = query.Include(p => p.Columns);

        if (includeIssues)
        {
            var twoWeeksAgo = DateTime.UtcNow.AddDays(-14);
            if (includeTeam)
                return await query
                    .Include(p => p.Columns)
                        .ThenInclude(c => c.NextColumnRelations)
                    .Include(p => p.Columns)
                        .ThenIncludeIssues(twoWeeksAgo)
                    .Include(p => p.Team)
                        .ThenInclude(t => t.TeamUserProfiles)
                            .ThenInclude(tup => tup.UserProfile)
                                .ThenInclude(up => up.User)
                    .FirstOrDefaultAsync(p => p.Id == id, token);
            else
                return await query
                    .Include(p => p.Columns)
                        .ThenInclude(c => c.NextColumnRelations)
                    .Include(p => p.Columns)
                        .ThenIncludeIssues(twoWeeksAgo)
                            .ThenInclude(i => i.Assignee)
                                .ThenInclude(up => up.User)
                    .Include(p => p.Creator)
                        .ThenInclude(up => up.User)
                    .FirstOrDefaultAsync(p => p.Id == id, token);
        }

        if (includeTeam)
            return await query.Include(p => p.Team)
                .ThenInclude(t => t.TeamUserProfiles)
                    .ThenInclude(tup => tup.UserProfile)
                        .ThenInclude(up => up.User)
                            .FirstOrDefaultAsync(p => p.Id == id, token);

        return await query
            .Include(p => p.Creator)
                .ThenInclude(up => up.User)
            .FirstOrDefaultAsync(p => p.Id == id, token);
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


    public async Task<IEnumerable<Project>> GetAllAsync(CancellationToken token = default)
    {
        return await context.Projects.ToListAsync(token);
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

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await context.SaveChangesAsync(token);
    }
}

public static class QueryableExtensions
{
    public static IIncludableQueryable<TEntity, IEnumerable<Issue>> ThenIncludeIssues<TEntity>(
        this IIncludableQueryable<TEntity, IEnumerable<Column>> query,
        DateTime twoWeeksAgo) where TEntity : class
    {
        return query.ThenInclude(c => c.Issues.Where(i =>
            i.ClosedAt == null || i.ClosedAt > twoWeeksAgo));
    }

    public static IIncludableQueryable<Column, IEnumerable<Issue>> ThenIncludeIssues(
        this IQueryable<Column> query, DateTime twoWeeksAgo)
    {
        return query.Include(c => c.Issues.Where(i =>
            i.ClosedAt == null || i.ClosedAt > twoWeeksAgo));
    }
}