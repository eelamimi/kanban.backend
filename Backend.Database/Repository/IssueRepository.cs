namespace Backend.Database.Repository;

public class IssueRepository(ApplicationDbContext context) : IIssueRepository
{
    private static readonly SemaphoreSlim _lock = new(1, 1);

    public async Task<Issue> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await TryGetByIdAsync(id, token) ?? throw new NullReferenceException("Issue is null");
    }

    public async Task<Issue?> TryGetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await context.Issues
            .FirstOrDefaultAsync(c => c.Id == id, token);
    }

    public async Task<IEnumerable<Issue>> GetAllAsync(CancellationToken token = default)
    {
        return await context.Issues.ToListAsync(token);
    }

    public async Task<int> GetNextNumberInProjectAsync(Guid projectId, CancellationToken token = default)
    {
        await _lock.WaitAsync(token);
        try
        {
            var maxNumber = await context.Issues
                .IgnoreQueryFilters()
                .Where(i => i.ProjectId == projectId)
                .MaxAsync(i => (int?)i.NumberInProject, token) ?? 0;

            return maxNumber + 1;
        }
        finally
        {
            _lock.Release();
        }
    }

    public void Add(Issue issue)
    {
        context.Issues.Add(issue);
    }

    public void Update(Issue issue)
    {
        context.Issues.Update(issue);
    }

    public void Remove(Issue issue)
    {
        context.Issues.Remove(issue);
    }

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await context.SaveChangesAsync(token);
    }
}