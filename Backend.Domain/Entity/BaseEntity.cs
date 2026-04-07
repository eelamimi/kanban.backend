namespace Backend.Domain.Entity;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}