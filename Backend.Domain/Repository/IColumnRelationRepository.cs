namespace Backend.Domain.Repository;

public interface IColumnRelationRepository
{
    Task<IEnumerable<ColumnRelation>> GetAllPrevByNextAsync(Guid nextId, CancellationToken cancellationToken = default);

    Task<IEnumerable<ColumnRelation>> GetAllNextByPrevAsync(Guid prevId, CancellationToken cancellationToken = default);

    void Add(ColumnRelation columnRelation);

    void Update(ColumnRelation columnRelation);

    void Remove(ColumnRelation columnRelation);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
