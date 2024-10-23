using System.ComponentModel.DataAnnotations;

namespace SoftPlex.Task.Core.Data.Models.Common;

public abstract class BaseEntity : IEntity
{
    public Guid Id { get; set; }
}