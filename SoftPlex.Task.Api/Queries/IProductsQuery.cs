using SoftPlex.Task.Core.Domain.Dto;

namespace SoftPlex.Task.Api.Queries;

public interface IProductsQuery
{
    Task<ProductDto[]> GetAllAsync(CancellationToken cancel = default);
    
    Task<ProductDto[]> GetByFilterNameAsync(string? name, CancellationToken cancel = default);
    
    Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancel = default);
}