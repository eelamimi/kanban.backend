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

    public void Add(ColumnRelation columnRelation)
    {
        context.ColumnRelations.Add(columnRelation);
    }

    public void Update(ColumnRelation columnRelation)
    {
        context.ColumnRelations.Update(columnRelation);
    }

    public void Remove(ColumnRelation columnRelation)
    {
        context.ColumnRelations.Remove(columnRelation);
    }

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await context.SaveChangesAsync(token);
    }
}