namespace Backend.Domain.Repository;

public interface ITeamRepository
{
    Task<Team> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Team?> TryGetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Team>> GetAllAsync(CancellationToken cancellationToken = default);

    void Add(Team team);

    void Update(Team team);

    void Remove(Team team);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
