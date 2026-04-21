namespace Backend.Domain.Entity;

public class Attachment : BaseEntity
{
    public virtual Issue Issue { get; set; }

    public Guid IssueId { get; set; }

    public virtual Commentary Commentary { get; set; }

    public Guid CommentaryId { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public long Size { get; set; }

    public byte[] Content { get; set; } = [];
}
