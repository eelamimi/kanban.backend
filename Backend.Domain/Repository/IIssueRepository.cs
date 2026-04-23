namespace Backend.Domain.Repository;

public interface IIssueRepository
{
    Task<Issue> GetByIdAsync(Guid id, bool withCommentaries = false, CancellationToken token = default);

    Task<Issue?> TryGetByIdAsync(Guid id, bool withCommentaries = false, CancellationToken token = default);

    Task<Issue> GetByNumberInProjectAndProjectIdsAsync(int numberInProject, Guid projectId, bool withCommentaries = false, CancellationToken token = default);

    Task<IEnumerable<Issue>> GetAllAsync(CancellationToken token = default);

    Task<int> GetNextNumberInProjectAsync(Guid projectId, CancellationToken token = default);

    Task SetDeletedByColumnIdAsync(Guid columnId, bool isDeleted = false, CancellationToken token = default);

    void Add(Issue issue);

    void Update(Issue issue);

    void Remove(Issue issue);

    Task<int> SaveChangesAsync(CancellationToken token = default);
}
