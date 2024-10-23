using SoftPlex.Task.Core.Data.Models.Common;

namespace SoftPlex.Task.Core.Data.Models;

/// <summary>
/// Версия изделия
/// </summary>
public class ProductVersion : BaseEntity
{
    /// <summary>
    /// Отношение к изделию
    /// </summary>
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; } = null!;

    /// <summary>
    /// Наименование версии изделия
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Описание версии изделия, не обязательно к заполнению
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Дата создания изделия
    /// </summary>
    public DateTime CreatingDate { get; set; }

    /// <summary>
    /// Габаритная ширина
    /// </summary>
    public double Width { get; set; }

    /// <summary>
    /// Габаритная высота
    /// </summary>
    public double Height { get; set; }

    /// <summary>
    /// Габаритная длинна
    /// </summary>
    public double Length { get; set; }
}