namespace Backend.Database.Repository;

public class ColumnRepository(ApplicationDbContext context) : IColumnRepository
{
    public async Task<Column> GetByIdAsync(Guid id, bool withNextColumns = false, bool withProject = false, CancellationToken token = default)
    {
        return await TryGetByIdAsync(id, withNextColumns, withProject, token)
            ?? throw new NullReferenceException("Column is null");
    }

    public async Task<Column?> TryGetByIdAsync(Guid id, bool withNextColumns = false, bool withProject = false, CancellationToken token = default)
    {
        var query = context.Columns.AsQueryable();

        if (withNextColumns)
            query = query.Include(c => c.NextColumnRelations);

        if (withProject)
            query = query.Include(c => c.Project)
                .ThenInclude(p => p.Columns);

        return await query
            .FirstOrDefaultAsync(c => c.Id == id, token);
    }

    public async Task<IEnumerable<Column>> GetAllAsync(CancellationToken token = default)
    {
        return await context.Columns.ToListAsync(token);
    }

    public void Add(Column column)
    {
        context.Columns.Add(column);
    }

    public void Update(Column column)
    {
        context.Columns.Update(column);
    }

    public void Remove(Column column)
    {
        context.Columns.Remove(column);
    }

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await context.SaveChangesAsync(token);
    }
}