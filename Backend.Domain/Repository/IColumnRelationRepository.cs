namespace Backend.Domain.Repository;

public interface IColumnRelationRepository
{
    Task<IEnumerable<ColumnRelation>> GetAllPrevByNextAsync(Guid nextId, CancellationToken token = default);

    Task<IEnumerable<ColumnRelation>> GetAllNextByPrevAsync(Guid prevId, CancellationToken token = default);

    void Add(ColumnRelation columnRelation);

    void Update(ColumnRelation columnRelation);

    void Remove(ColumnRelation columnRelation);

    Task<int> SaveChangesAsync(CancellationToken token = default);
}
