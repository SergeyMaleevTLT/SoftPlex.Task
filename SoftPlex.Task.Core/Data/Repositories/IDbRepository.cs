

using SoftPlex.Task.Core.Data.Models.Common;

namespace SoftPlex.Task.Core.Data.Repositories;

/// <summary>
/// Минимальный интерфейс взаимодействия с БД
/// </summary>
public interface IDbRepository
{
    IQueryable<T> GetAll<T>(CancellationToken cancel = default) where T : class, IEntity;
       
    Task<T> Add<T>(T item, CancellationToken cancel = default) where T : class, IEntity;
               
    Task<T> Update<T>(T item, CancellationToken cancel = default) where T : class, IEntity;
    
    System.Threading.Tasks.Task Remove<T>(Guid itemId, CancellationToken cancel = default) where T : class, IEntity;
}