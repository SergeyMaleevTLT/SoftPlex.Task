namespace SoftPlex.Task.Core.Domain.Models.Common;

/// <summary>
/// Минимальный интнрфейс всех сущностей в БД
/// </summary>
public interface IEntity
{
    Guid Id { get; set; }
}