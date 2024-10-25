using Microsoft.Extensions.DependencyInjection;
using SoftPlex.Task.Core.Domain.Models;
using SoftPlex.Task.Core.Infrastructure.Data.Context;
using SoftPlex.Task.Core.Infrastructure.Data.Context.Impl.PostgreSQL;
using SoftPlex.Task.Core.Infrastructure.Data.Repositories;


namespace SoftPlex.Task.Core.Infrastructure.Data;

public static class DbInstallerExtensions
{
    /// <summary>
    /// Инициализирует репозиторий данных IDbRepository<Product>
    /// </summary>
    public static void AddDataRepository(this IServiceCollection services, ConnectionStringsSection settings)
    {
        var dbProviders = typeof(ConnectionStringsSection).Assembly
            .GetTypes().Where(t => !t.IsInterface && !t.IsAbstract && typeof(IMainEfDbProvider).IsAssignableFrom(t)).ToArray();

        var providerName = settings.Provider ?? nameof(PostgreSqlEfDbProvider);
        var providerType = dbProviders.FirstOrDefault(p => p.Name == providerName);

        if (providerType == null)
        {
            throw new ArgumentNullException($"DbProvider '{providerName}' not found");
        }

        var provider = Activator.CreateInstance(providerType) as IMainEfDbProvider;
       
        if (provider == null)
        {
            throw new ArgumentNullException($"DbProvider '{providerType}' is null");
        }

        provider.InitContext<DataContext>(settings, services);
        services.AddScoped<IDbRepository<Product>, ProductRepository>();
    }
}