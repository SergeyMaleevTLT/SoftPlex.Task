using Microsoft.EntityFrameworkCore;
using SoftPlex.Task.Core.Data.Context;
using SoftPlex.Task.Core.Data.Models.Common;

namespace SoftPlex.Task.Core.Data.Repositories;

public class EfRepository : IDbRepository
{
    private readonly DataContext _dataContext;
    
    public EfRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<T> Add<T>(T item, CancellationToken cancel = default) where T : class, IEntity
    {
        if (item is null) throw new ArgumentNullException(nameof(item));

        await _dataContext.Set<T>().AddAsync(item, cancel);
        await _dataContext.SaveChangesAsync(cancel);
        return item;

    }

    public IQueryable<T> GetAll<T>(CancellationToken cancel = default) where T : class, IEntity
    {
        return _dataContext.Set<T>().AsNoTracking().AsQueryable();
    }

    public async Task<T> Update<T>(T item, CancellationToken cancel) where T : class, IEntity
    {
        if (item is null) throw new ArgumentNullException(nameof(item));

        _dataContext.Update(item);
        await _dataContext.SaveChangesAsync(cancel);
        return item;
    }

    public async System.Threading.Tasks.Task Remove<T>(Guid itemId, CancellationToken cancel = default) where T : class, IEntity
    {
        var item = await _dataContext.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == itemId, cancel);
        
        if (item is not null)
        {
            _dataContext.Set<T>().Remove(item);
            await _dataContext.SaveChangesAsync(cancel);
        }
    }
}