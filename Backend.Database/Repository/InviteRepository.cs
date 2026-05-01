namespace Backend.Database.Repository;

public class InviteRepository(ApplicationDbContext context) : IInviteRepository
{
    public async Task<Invite> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await TryGetByIdAsync(id, token) 
            ?? throw new NullReferenceException("Invite not found");
    }

    public async Task<Invite?> TryGetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await context.Invites.FirstOrDefaultAsync(x => x.Id == id, token);
    }

    public async void Add(Invite invite)
    {
        context.Add(invite);
    }

    public async void Update(Invite invite)
    {
        context.Update(invite);
    }

    public async void Remove(Invite invite)
    {
        context.Remove(invite);
    }

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await context.SaveChangesAsync(token);
    }
}
