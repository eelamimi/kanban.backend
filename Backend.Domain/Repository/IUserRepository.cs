namespace Backend.Domain.Repository;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id, bool includeProfile = true, CancellationToken cancellationToken = default);

    Task<User?> TryGetByIdAsync(Guid id, bool includeProfile = true, CancellationToken cancellationToken = default);

    Task<User?> TryGetByEmailAsync(string email, bool includeProfile = true, CancellationToken cancellationToken = default);

    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);

    void Add(User user);

    void Update(User user);

    void Remove(User user);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
