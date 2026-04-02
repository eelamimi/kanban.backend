namespace Backend.Domain.Repository;

public interface IUserProfileRepository
{
    Task<UserProfile> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<UserProfile?> TryGetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<UserProfile>> GetAllAsync(CancellationToken cancellationToken = default);

    void Add(UserProfile userProfile);

    void Update(UserProfile userProfile);

    void Remove(UserProfile userProfile);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
