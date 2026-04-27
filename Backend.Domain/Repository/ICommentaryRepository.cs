namespace Backend.Domain.Repository;

public interface ICommentaryRepository
{
    Task<Commentary> GetByIdAsync(Guid id, bool withAuthor = false, CancellationToken token = default);

    Task<Commentary?> TryGetByIdAsync(Guid id, bool withAuthor = false, CancellationToken token = default);

    Task<Commentary> GetDescriptionAsync(Guid issueId, CancellationToken token = default);

    Task<IEnumerable<Commentary>> GetAllAsync(CancellationToken token = default);

    void Add(Commentary commentary);

    void Update(Commentary commentary);

    void Remove(Commentary commentary);

    Task<int> SaveChangesAsync(CancellationToken token = default);
}
