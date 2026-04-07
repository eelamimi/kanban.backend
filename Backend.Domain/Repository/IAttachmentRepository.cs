namespace Backend.Domain.Repository;

public interface IAttachmentRepository
{
    Task<Attachment> GetByIdAsync(Guid id, CancellationToken token = default);

    Task<Attachment?> TryGetByIdAsync(Guid id, CancellationToken token = default);

    Task<IEnumerable<Attachment>> GetAllAsync(CancellationToken token = default);

    void Add(Attachment attachment);

    void Update(Attachment attachment);

    void Remove(Attachment attachment);

    Task<int> SaveChangesAsync(CancellationToken token = default);
}
