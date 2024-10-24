using Microsoft.EntityFrameworkCore;
using SoftPlex.Task.Core.Domain.Models;
using SoftPlex.Task.Core.Infrastructure.Data.Context;

namespace SoftPlex.Task.Core.Infrastructure.Data.Repositories;

public class ProductRepository : IDbRepository<Product>
{
    private readonly DataContext _dataContext;
    
    
    
    public ProductRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public IQueryable<Product> GetAll(CancellationToken cancel = default)
    {
        return _dataContext.Product.AsNoTracking().AsQueryable();
    }

    public async Task<Product> Add(Product item, CancellationToken cancel = default)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));

        await _dataContext.Product.AddAsync(item, cancel);
        await _dataContext.SaveChangesAsync(cancel);
        return item;
    }

    public async Task<Product> Update(Product item, CancellationToken cancel = default)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));

        _dataContext.Product.Update(item);
        await _dataContext.SaveChangesAsync(cancel);
        return item;
    }

    public async System.Threading.Tasks.Task Remove(Product item, CancellationToken cancel = default)
    {
        _dataContext.Product.Remove(item);
        await _dataContext.SaveChangesAsync(cancel);
    }
}