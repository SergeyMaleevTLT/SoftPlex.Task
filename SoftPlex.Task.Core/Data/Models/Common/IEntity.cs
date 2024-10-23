namespace SoftPlex.Task.Core.Data.Models.Common;

/// <summary>
/// Минимальный интнрфейс всех сущностей в БД
/// </summary>
public interface IEntity
{
    Guid Id { get; set; }
}