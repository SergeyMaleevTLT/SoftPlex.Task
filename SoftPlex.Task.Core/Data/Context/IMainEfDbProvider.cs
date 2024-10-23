using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SoftPlex.Task.Core.Data.Context;

/// <summary>
/// Провайдер Entity FrameWork БД, который инициализирует БД
/// </summary>
public interface IMainEfDbProvider
{
    void InitContext<TContext>(ConnectionStringsSection settings, IServiceCollection services)
        where TContext : DbContext;
    
    public string ProviderName { get;  }
}