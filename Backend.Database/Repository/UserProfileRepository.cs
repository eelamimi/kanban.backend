namespace Backend.Database.Repository;

public class UserProfileRepository(ApplicationDbContext context) : IUserProfileRepository
{
    public async Task<UserProfile> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await TryGetByIdAsync(id, cancellationToken) ?? throw new NullReferenceException("UserProfile is null");
    }

    public async Task<UserProfile?> TryGetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.UserProfiles
            .Include(up => up.User)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<UserProfile>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.UserProfiles.ToListAsync(cancellationToken);
    }

    public void Add(UserProfile userProfile)
    {
        context.UserProfiles.Add(userProfile);
    }

    public void Update(UserProfile userProfile)
    {
        context.UserProfiles.Update(userProfile);
    }

    public void Remove(UserProfile userProfile)
    {
        context.UserProfiles.Remove(userProfile);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}