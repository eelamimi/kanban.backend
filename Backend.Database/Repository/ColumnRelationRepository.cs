namespace Backend.Database.Repository;

public class ColumnRelationRepository(ApplicationDbContext context) : IColumnRelationRepository
{
    public Task<IEnumerable<ColumnRelation>> GetAllPrevByNextAsync(Guid nextId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ColumnRelation>> GetAllNextByPrevAsync(Guid prevId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ColumnRelation>> GetAllAsync(CancellationToken token = default)
    {
        return await context.ColumnRelations.ToListAsync(token);
    }

    public void Add(ColumnRelation column)
    {
        context.ColumnRelations.Add(column);
    }

    public void Update(ColumnRelation column)
    {
        context.ColumnRelations.Update(column);
    }

    public void Remove(ColumnRelation column)
    {
        context.ColumnRelations.Remove(column);
    }

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await context.SaveChangesAsync(token);
    }
}