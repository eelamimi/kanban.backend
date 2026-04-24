namespace Backend.Application.Queries.Response;

public class CommentaryResponse
{
    public Guid Id { get; init; }

    public UserResponse Author { get; init; }

    public string Content { get; init; } = string.Empty;

    public bool IsDescription { get; init; } = false;

    public DateTime CreatedAt { get; init; }

    public DateTime? LastEditedAt { get; init; }

    public bool IsEdited { get; init; }
}
