namespace Backend.Domain.Repository;

public interface IProjectRepository
{
    Task<Project> GetByIdAsync(Guid id, bool includeIssues = false, bool includeTeam = false, CancellationToken cancellationToken = default);

    Task<Project?> TryGetByIdAsync(Guid id, bool includeIssues = false, bool includeTeam = false, CancellationToken cancellationToken = default  );

    Task<IEnumerable<Project>> GetAllByTeamIdAsync(Guid teamId, CancellationToken token = default);

    Task<IEnumerable<Project>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> HasColumnName(Guid id, string columnName, CancellationToken token = default);

    void Add(Project project);

    void Update(Project project);

    void Remove(Project project);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
