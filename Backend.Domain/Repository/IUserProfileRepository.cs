namespace Backend.Domain.Repository;

public interface IUserProfileRepository
{
    Task<UserProfile> GetByIdAsync(Guid id, CancellationToken token = default);

    Task<UserProfile?> TryGetByIdAsync(Guid id, CancellationToken token = default);

    Task<IEnumerable<UserProfile>> GetAllAsync(CancellationToken token = default);

    void Add(UserProfile userProfile);

    void Update(UserProfile userProfile);

    void Remove(UserProfile userProfile);

    Task<int> SaveChangesAsync(CancellationToken token = default);
}
