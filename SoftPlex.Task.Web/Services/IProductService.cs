using SoftPlex.Task.Core.Domain.Dto;

namespace SoftPlex.Task.Web.Services;

public interface IProductService
{
    Task<ProductDto> GetProductByIdAsync(Guid id, CancellationToken token = default);
    Task<ProductDto[]> GetProductsSearchByNameAsync(string search, CancellationToken token = default);
    Task<ProductDto> AddProductAsync(ProductPostDto product, CancellationToken token = default);
    Task<ProductDto> UpdateProductAsync(ProductDto product, CancellationToken token = default);
    Task<bool> DeleteProductAsync(Guid productId, CancellationToken token = default);
}