namespace Backend.Domain.Repository;

public interface IColumnRepository
{
    Task<Column> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Column?> TryGetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Column>> GetAllAsync(CancellationToken cancellationToken = default);

    void Add(Column column);

    void Update(Column column);

    void Remove(Column column);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
