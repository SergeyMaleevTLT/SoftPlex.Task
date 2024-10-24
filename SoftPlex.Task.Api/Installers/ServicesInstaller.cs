using System.Reflection;
using SoftPlex.Task.Api.Queries;

namespace SoftPlex.Task.API.Installers;

public class ServicesInstaller: IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IProductsQuery, ProductsQuery>();
    }

}