namespace Backend.Application.Queries.Response;

public class AttachmentResponse
{
    public Guid Id { get; init; }

    public string FileName { get; init; } = string.Empty;

    public long Size { get; init; }

    public byte[] Content { get; init; } = [];
}
