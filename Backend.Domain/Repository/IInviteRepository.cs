namespace Backend.Domain.Repository;

public interface IInviteRepository
{
    Task<Invite> GetByIdAsync(Guid id, CancellationToken token = default);

    Task<Invite?> TryGetByIdAsync(Guid id, CancellationToken token = default);

    Task<string?> GetLastByTeamAndRoleAsync(Guid teamId, Guid roleId, CancellationToken token = default);

    Task<Invite> GeByTokenAsync(string inviteToken, CancellationToken token = default);

    void Add(Invite invite);

    void Update(Invite invite);

    void Remove(Invite invite);

    Task<int> SaveChangesAsync(CancellationToken token = default);
}
