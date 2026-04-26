namespace Backend.Database.Repository;

public class CommentaryRepository(ApplicationDbContext context) : ICommentaryRepository
{
    public async Task<Commentary> GetByIdAsync(Guid id, bool withAuthor = false, CancellationToken token = default)
    {
        return await TryGetByIdAsync(id, true, token) ?? throw new NullReferenceException("Commentary is null");
    }

    public async Task<Commentary?> TryGetByIdAsync(Guid id, bool withAuthor = false, CancellationToken token = default)
    {
        var query = context.Commentaries.AsQueryable();

        if (withAuthor)
            query = query
                .Include(c => c.Author)
                    .ThenInclude(up => up.User);

        return await query
            .FirstOrDefaultAsync(c => c.Id == id, token);
    }

    public async Task<IEnumerable<Commentary>> GetAllAsync(CancellationToken token = default)
    {
        return await context.Commentaries.ToListAsync(token);
    }

    public void Add(Commentary commentary)
    {
        context.Commentaries.Add(commentary);
    }

    public void Update(Commentary commentary)
    {
        context.Commentaries.Update(commentary);
    }

    public void Remove(Commentary commentary)
    {
        context.Commentaries.Remove(commentary);
    }

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await context.SaveChangesAsync(token);
    }
}