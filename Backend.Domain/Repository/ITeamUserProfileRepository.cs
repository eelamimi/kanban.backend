namespace Backend.Domain.Repository;

public interface ITeamUserProfileRepository
{
    Task<IEnumerable<TeamUserProfile>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<TeamUserProfile>> GetTeamsByUserProfileIdAsync(Guid userProfileId, CancellationToken token = default);

    Task<IEnumerable<TeamUserProfile>> GetUsersByTeamIdAsync(Guid teamId, CancellationToken token = default);

    Task<bool> IsSameTeam(Guid firstUserProfile, Guid secondUserProfile, CancellationToken token = default);

    Task<bool> IsInTeam(Guid userProfileId, Guid teamId, CancellationToken token = default);

    Task<bool> IsInProject(Guid userProfileId, Guid projectId, CancellationToken token = default);

    void Add(TeamUserProfile teamUserProfile);

    void Update(TeamUserProfile teamUserProfile);

    void Remove(TeamUserProfile teamUserProfile);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
