namespace Backend.Domain.Entity;

public class Attachment : BaseEntity
{
    public virtual Commentary? Commentary { get; set; }

    public Guid CommentaryId { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public long Size { get; set; }

    public byte[] Content { get; set; } = [];
}
