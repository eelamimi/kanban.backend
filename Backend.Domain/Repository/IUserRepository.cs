namespace Backend.Domain.Repository;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id, bool includeProfile = true, CancellationToken token = default);

    Task<User?> TryGetByIdAsync(Guid id, bool includeProfile = true, CancellationToken token = default);

    Task<User?> TryGetByEmailAsync(string email, bool includeProfile = true, CancellationToken token = default);

    Task<IEnumerable<User>> GetAllAsync(CancellationToken token = default);

    Task<bool> ExistsByEmailAsync(string email, CancellationToken token = default);

    void Add(User user);

    void Update(User user);

    void Remove(User user);

    Task<int> SaveChangesAsync(CancellationToken token = default);
}
