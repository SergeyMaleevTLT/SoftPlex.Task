namespace SoftPlex.Task.Web.Infrastructure;

public static class Api
{
    public static class Product
    {
        public static string GetProductById(string baseUri, Guid productId)
        {
            return $"{baseUri}/GetById/{productId}";
        }

        public static string GetProducts(string baseUri, string? search = null)
        {
            return search is not null
                ? $"{baseUri}?searchByName={search}"
                : $"{baseUri}";
        }

        public static string InsertOrUpdateProduct(string baseUri)
        {
            return $"{baseUri}";
        }
        
        public static string DeleteProductById(string baseUri, Guid productId)
        {
            return $"{baseUri}/{productId}";
        }
    }
}