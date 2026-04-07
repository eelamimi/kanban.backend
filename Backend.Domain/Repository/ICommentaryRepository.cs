namespace Backend.Domain.Repository;

public interface ICommentaryRepository
{
    Task<Commentary> GetByIdAsync(Guid id, CancellationToken token = default);

    Task<Commentary?> TryGetByIdAsync(Guid id, CancellationToken token = default);

    Task<IEnumerable<Commentary>> GetAllAsync(CancellationToken token = default);

    void Add(Commentary commentary);

    void Update(Commentary commentary);

    void Remove(Commentary commentary);

    Task<int> SaveChangesAsync(CancellationToken token = default);
}
