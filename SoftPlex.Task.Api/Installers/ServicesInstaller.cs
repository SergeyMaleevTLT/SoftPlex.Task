using SoftPlex.Task.Api.Queries;

namespace SoftPlex.Task.API.Installers;

public class ServicesInstaller: IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductsQuery, ProductsQuery>();
        
    }

}