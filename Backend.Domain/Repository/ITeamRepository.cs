namespace Backend.Domain.Repository;

public interface ITeamRepository
{
    Task<Team> GetByIdAsync(Guid id, CancellationToken token = default);

    Task<Team?> TryGetByIdAsync(Guid id, CancellationToken token = default);

    Task<IEnumerable<Team>> GetAllAsync(CancellationToken token = default);

    void Add(Team team);

    void Update(Team team);

    void Remove(Team team);

    Task<int> SaveChangesAsync(CancellationToken token = default);
}
