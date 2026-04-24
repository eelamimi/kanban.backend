namespace Backend.Application.Map;

public static class CommentaryResponseMap
{
    public static CommentaryResponse Map(this Commentary commentary)
    {
        return new CommentaryResponse
        {
            Id = commentary.Id,
            Author = commentary.Author.Map(),
            Content = commentary.Content,
            IsDescription = commentary.IsDescription,
            CreatedAt = commentary.CreatedAt,
            LastEditedAt = commentary.LastEditedAt,
            IsEdited = commentary.CreatedAt != commentary.LastEditedAt,
        };
    }
}
