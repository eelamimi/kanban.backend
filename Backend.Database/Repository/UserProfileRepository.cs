namespace Backend.Database.Repository;

public class UserProfileRepository(ApplicationDbContext context) : IUserProfileRepository
{
    public async Task<UserProfile> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await TryGetByIdAsync(id, token) ?? throw new NullReferenceException("UserProfile is null");
    }

    public async Task<UserProfile?> TryGetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await context.UserProfiles
            .Include(up => up.User)
            .FirstOrDefaultAsync(u => u.Id == id, token);
    }

    public async Task<IEnumerable<UserProfile>> GetAllAsync(CancellationToken token = default)
    {
        return await context.UserProfiles.ToListAsync(token);
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

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await context.SaveChangesAsync(token);
    }
}