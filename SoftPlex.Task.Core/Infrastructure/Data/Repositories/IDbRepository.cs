using SoftPlex.Task.Core.Domain.Models.Common;

namespace SoftPlex.Task.Core.Infrastructure.Data.Repositories;

/// <summary>
/// Минимальный интерфейс взаимодействия с БД
/// </summary>
public interface IDbRepository<T> where T : IEntity
{
    IQueryable<T> GetAll(CancellationToken cancel = default);
       
    Task<T> Add(T item, CancellationToken cancel = default);
               
    Task<T> Update(T item, CancellationToken cancel = default);
    
    System.Threading.Tasks.Task Remove(T item, CancellationToken cancel = default);
}