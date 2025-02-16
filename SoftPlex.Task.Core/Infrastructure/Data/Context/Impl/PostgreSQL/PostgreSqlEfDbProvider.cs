﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace SoftPlex.Task.Core.Infrastructure.Data.Context.Impl.PostgreSQL;

public class PostgreSqlEfDbProvider  : IMainEfDbProvider
{
    public void InitContext<TContext>(ConnectionStringsSection settings, IServiceCollection services) where TContext : DbContext
    {
        
        var builder = new NpgsqlDataSourceBuilder(settings.Data);
        var dbSource = builder.Build();

        services.AddEntityFrameworkNpgsql()
            .AddDbContextPool<DataContext>(options =>
                options.UseNpgsql(dbSource)).AddLogging();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public string ProviderName => nameof(PostgreSqlEfDbProvider);
}