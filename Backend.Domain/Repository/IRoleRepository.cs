namespace Backend.Domain.Repository;

public interface IRoleRepository
{
    Task<Role> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Role?> TryGetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken = default);

    void Add(Role role);

    void Update(Role role);

    void Remove(Role role);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
