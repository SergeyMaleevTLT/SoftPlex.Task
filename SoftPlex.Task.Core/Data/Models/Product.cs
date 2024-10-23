using SoftPlex.Task.Core.Data.Models.Common;

namespace SoftPlex.Task.Core.Data.Models;

/// <summary>
/// Изделие
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Наименование изделия
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Описание изделия, не обязательно к заполнению
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Версии изделия
    /// </summary>
    public ICollection<ProductVersion>? ProductVersionSet { get; set; }
}