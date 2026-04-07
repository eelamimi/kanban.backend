namespace Backend.Database.Repository;

public class RoleRepository(ApplicationDbContext context) : IRoleRepository
{
    public async Task<Role> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await TryGetByIdAsync(id, token) ?? throw new NullReferenceException("Role is null");
    }

    public async Task<Role?> TryGetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await context.Roles
            .FirstOrDefaultAsync(r => r.Id == id, token);
    }

    public async Task<IEnumerable<Role>> GetAllAsync(CancellationToken token = default)
    {
        return await context.Roles.ToListAsync(token);
    }

    public void Add(Role role)
    {
        context.Roles.Add(role);
    }

    public void Update(Role role)
    {
        context.Roles.Update(role);
    }

    public void Remove(Role role)
    {
        context.Roles.Remove(role);
    }

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await context.SaveChangesAsync(token);
    }
}