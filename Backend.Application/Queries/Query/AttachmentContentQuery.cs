namespace Backend.Application.Queries.Query;

public class AttachmentContentQuery : ICommand<byte[]>
{
    public Guid UserProfileId { get; init; }

    public Guid ProjectId { get; init; }

    public Guid AttachmentId { get; init; }
}
