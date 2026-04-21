namespace Backend.Application.Map;

public static class AttachmentResponseMap
{
    public static AttachmentResponse Map(this Attachment attachment)
    {
        return new AttachmentResponse
        {
            Id = attachment.Id,
            FileName = attachment.FileName,
            Size = attachment.Size,
            Content = attachment.Content,
        };
    }
}
