namespace SoftPlex.Task.Core.Domain.Models.Common;

public abstract class BaseEntity : IEntity
{
    public Guid Id { get; set; }
}