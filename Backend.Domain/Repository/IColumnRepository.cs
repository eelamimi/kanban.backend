namespace Backend.Domain.Repository;

public interface IColumnRepository
{
    Task<Column> GetByIdAsync(Guid id, bool withNextColumns = false, bool withProject = false, CancellationToken token = default);

    Task<Column?> TryGetByIdAsync(Guid id, bool withNextColumns = false, bool withProject = false, CancellationToken token = default);

    Task<IEnumerable<Column>> GetAllAsync(CancellationToken token = default);

    void Add(Column column);

    void Update(Column column);

    void Remove(Column column);

    Task<int> SaveChangesAsync(CancellationToken token = default);
}
