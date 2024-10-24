using SoftPlex.Task.Core.Domain.Dto;
using SoftPlex.Task.Web.Infrastructure;

namespace SoftPlex.Task.Web.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly string _remoteServiceBaseUrl;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _remoteServiceBaseUrl = _httpClient.BaseAddress + "api/products";
    }

    public async Task<ProductDto> GetProductByIdAsync(Guid id, CancellationToken token = default)
    {
        var uri = Api.Product.GetProductById(_remoteServiceBaseUrl, id);

        var response = await _httpClient.GetAsync(uri, token);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<ProductDto>(cancellationToken: token)
                   ?? throw new Exception($"Content type {nameof(ProductDto)} error");
        
        throw new Exception(await response.Content.ReadAsStringAsync(token));
    }

    public async Task<ProductDto[]> GetProductsSearchByNameAsync(string search, CancellationToken token = default)
    {
        var uri = Api.Product.GetProducts(_remoteServiceBaseUrl, search);

        var response = await _httpClient.GetAsync(uri, token);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<ProductDto[]>(cancellationToken: token)
                   ?? throw new Exception($"Content type array {nameof(ProductDto)} error");;
        
        throw new Exception(await response.Content.ReadAsStringAsync(token));
    }

    public async Task<ProductDto> AddProductAsync(ProductPostDto product, CancellationToken token = default)
    {
        var uri = Api.Product.InsertOrUpdateProduct(_remoteServiceBaseUrl);
        var response = await _httpClient.PostAsJsonAsync(uri, product, token);
        
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<ProductDto>(cancellationToken: token)
                   ?? throw new Exception($"Content type {nameof(ProductDto)} error");

        throw new Exception(await response.Content.ReadAsStringAsync(cancellationToken: token));
    }

    public async Task<ProductDto> UpdateProductAsync(ProductDto product, CancellationToken token = default)
    {
        var uri = Api.Product.InsertOrUpdateProduct(_remoteServiceBaseUrl);
        var response = await _httpClient.PutAsJsonAsync(uri, product, token);
        
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<ProductDto>(cancellationToken: token)
                   ?? throw new Exception($"Content type {nameof(ProductDto)} error");;

        throw new Exception(await response.Content.ReadAsStringAsync(cancellationToken: token));
    }

    public async Task<bool> DeleteProductAsync(Guid productId, CancellationToken token = default)
    {
        var uri = Api.Product.DeleteProductById(_remoteServiceBaseUrl, productId);
        var response = await _httpClient.DeleteAsync(uri, token);

        return response.IsSuccessStatusCode;
    }
}