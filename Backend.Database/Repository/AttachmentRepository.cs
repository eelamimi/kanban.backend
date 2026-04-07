namespace Backend.Database.Repository;

public class AttachmentRepository(ApplicationDbContext context) : IAttachmentRepository
{
    public async Task<Attachment> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await TryGetByIdAsync(id, token) ?? throw new NullReferenceException("Attachment is null");
    }

    public async Task<Attachment?> TryGetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await context.Attachments
            .FirstOrDefaultAsync(c => c.Id == id, token);
    }

    public async Task<IEnumerable<Attachment>> GetAllAsync(CancellationToken token = default)
    {
        return await context.Attachments.ToListAsync(token);
    }

    public void Add(Attachment attachment)
    {
        context.Attachments.Add(attachment);
    }

    public void Update(Attachment attachment)
    {
        context.Attachments.Update(attachment);
    }

    public void Remove(Attachment attachment)
    {
        context.Attachments.Remove(attachment);
    }

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await context.SaveChangesAsync(token);
    }
}