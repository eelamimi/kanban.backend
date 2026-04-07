namespace Backend.Database.Repository;

public class ColumnRepository(ApplicationDbContext context) : IColumnRepository
{
    public async Task<Column> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await TryGetByIdAsync(id, token) ?? throw new NullReferenceException("Column is null");
    }

    public async Task<Column?> TryGetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await context.Columns
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