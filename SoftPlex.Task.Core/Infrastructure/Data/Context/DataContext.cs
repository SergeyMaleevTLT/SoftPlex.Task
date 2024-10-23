using Microsoft.EntityFrameworkCore;
using SoftPlex.Task.Core.Domain.Models;
using SoftPlex.Task.Core.Domain.Models.Common;

namespace SoftPlex.Task.Core.Infrastructure.Data.Context;

public class DataContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductVersion> ProductVersions { get; set; }
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntity).Assembly);
        Apply(modelBuilder);
    }
    
     public void Apply(ModelBuilder modelBuilder)
     {

         var products = new Product[]
         {
             new () { Id = Guid.NewGuid(), Name = "Table", Description = "A sturdy and elegant table" },
             new () { Id = Guid.NewGuid(), Name = "Sofa", Description = "A soft and cozy sofa" },
             new () { Id = Guid.NewGuid(), Name = "Wardrobe", Description = "Spacious storage cabinet" },
             new () { Id = Guid.NewGuid(), Name = "Armchair", Description = "A comfortable lounge chair" }
         };
         
         modelBuilder.Entity<Product>().HasData(products);
         
         var productVersions = new ProductVersion[]
         {
             new ()
             {
                 Id = Guid.NewGuid(),
                 ProductId= products.First(x => x.Name == "Table").Id,
                 Name = "Oak",
                 Description = "A sturdy and elegant table",
                 CreatingDate = DateTime.Now,
                 Width = 120.0, 
                 Height = 70.0, 
                 Length = 90.0
             },
             new ()
             {
                 Id = Guid.NewGuid(),
                 ProductId = products.First(x => x.Name == "Sofa").Id,
                 Name = "Luxury",
                 Description = "Luxury sofa with upholstered upholstery",
                 CreatingDate = DateTime.Now,
                 Width = 220.0, 
                 Height = 20.0, 
                 Length = 100.0
             },
             new ()
             {
                 Id = Guid.NewGuid(),
                 ProductId = products.First(x => x.Name == "Wardrobe").Id,
                 Name = "Wardrobe classic",
                 Description = "Spacious wardrobe",
                 CreatingDate = DateTime.Now,
                 Width = 120.0, 
                 Height = 200.0, 
                 Length = 150.0
             },
             new ()
             {
                 Id = Guid.NewGuid(),
                 ProductId = products.First(x => x.Name == "Armchair").Id,
                 Name = "Recliner",
                 Description = "Ergonomic relaxation chair",
                 CreatingDate = DateTime.Now,
                 Width = 20.0, 
                 Height = 80.0, 
                 Length = 100.0
             },
         };
         modelBuilder.Entity<ProductVersion>().HasData(productVersions);
     }
}

