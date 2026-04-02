namespace Backend.Domain.Entity;

// **** how to add new entity ****
// 1. create entity with impl of BaseEntity
// 2. create new configuration
public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}