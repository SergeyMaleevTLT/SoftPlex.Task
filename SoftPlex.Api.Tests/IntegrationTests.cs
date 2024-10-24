using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using SoftPlex.Task.Core.Domain.Models;
using SoftPlex.Task.Core.Infrastructure.Data.Context;

namespace SoftPlex.Api.Tests;

public class IntegrationTests
{
    protected readonly HttpClient TestHttpClient;
    protected DataContext DataContext;
    
    protected readonly ConnectionStringsSection Options;

    protected IntegrationTests()
    {
        var appFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DataContext));
                
                var options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .EnableSensitiveDataLogging(true)
                    .Options;
                
                DataContext = new DataContext(options);
                DataContext.Database.EnsureCreated();
                
                services.AddSingleton(x => DataContext);
            });
            
            
        });

        Options = appFactory.Services.GetService<ConnectionStringsSection>() ?? throw new ArgumentException(nameof(ConnectionStringsSection));
        TestHttpClient = appFactory.CreateClient();
    }
}

public class ProductTestDataBase: DataContext
{
    public static Guid TestProductId = new("1d0ce7d3-1fbc-43f2-bf9d-8ef22002e7fe");
    
    public ProductTestDataBase(DbContextOptions<DataContext> options) : base (options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(new Product { Id = TestProductId, Name = "Test", Description = "Test Descriptoins" });
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        base.OnConfiguring(optionsBuilder);
    }
}