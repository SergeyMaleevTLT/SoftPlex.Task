using SoftPlex.Task.Core.Domain.Models.Common;

namespace SoftPlex.Task.Core.Domain.Models;

/// <summary>
/// Журнал событий
/// </summary>
public class EventLog : BaseEntity
{
    /// <summary>
    /// Дата события
    /// </summary>
    public DateTime EventDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Описание события
    /// </summary>
    public string? Description { get; set; }
}