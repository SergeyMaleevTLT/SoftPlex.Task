using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SoftPlex.Task.Core.Infrastructure.Data.Context.Impl.MSSQL;

public class MSSqlEfDbProvider : IMainEfDbProvider
{
    public void InitContext<TContext>(ConnectionStringsSection settings, IServiceCollection services) where TContext : DbContext
    {
        services.AddDbContextPool<TContext>(options =>
            {
                options.UseSqlServer(settings.Data);
            }
        );
    }
    
    public string ProviderName => nameof(MSSqlEfDbProvider);
}