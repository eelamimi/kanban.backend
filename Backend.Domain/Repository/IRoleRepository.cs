namespace Backend.Domain.Repository;

public interface IRoleRepository
{
    Task<Role> GetByIdAsync(Guid id, CancellationToken token = default);

    Task<Role?> TryGetByIdAsync(Guid id, CancellationToken token = default);

    Task<IEnumerable<Role>> GetAllAsync(CancellationToken token = default);

    void Add(Role role);

    void Update(Role role);

    void Remove(Role role);

    Task<int> SaveChangesAsync(CancellationToken token = default);
}
