namespace Backend.Database.Repository;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User> GetByIdAsync(Guid id, bool includeProfile = true, CancellationToken token = default)
    {
        var user = await TryGetByIdAsync(id, includeProfile, token);
        return user ?? throw new NullReferenceException("User is null");
    }

    public async Task<User?> TryGetByIdAsync(Guid id, bool includeProfile = true, CancellationToken token = default)
    {
        var query = context.Users.AsQueryable();

        if (includeProfile)
            query = query.Include(u => u.UserProfile);

        return await query
            .FirstOrDefaultAsync(u => u.Id == id, token);
    }

    public async Task<User?> TryGetByEmailAsync(string email, bool includeProfile = true, CancellationToken token = default)
    {
        var query = context.Users.AsQueryable();

        if (includeProfile)
            query = query.Include(u => u.UserProfile);

        return await query
            .FirstOrDefaultAsync(u => u.Email == email, token);
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken token = default)
    {
        return await context.Users.ToListAsync(token);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken token = default)
    {
        return await context.Users.AnyAsync(u => u.Email == email, token);
    }

    public void Add(User user)
    {
        context.Users.Add(user);
    }

    public void Update(User user)
    {
        context.Users.Update(user);
    }

    public void Remove(User user)
    {
        context.Users.Remove(user);
    }

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await context.SaveChangesAsync(token);
    }
}