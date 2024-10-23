using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftPlex.Task.Core.Data.Models;

namespace SoftPlex.Task.Core.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.HasKey(e => e.Id).HasName("PK__Product__3214EC275370E6CA");

            builder.HasIndex(e => e.Name, "IX_Product_Name");

            builder.HasIndex(e => e.Name, "UC_Product_Name").IsUnique();

            builder.Property(e => e.Id)
                    .HasColumnName("ID");

            builder.Property(e => e.Name).HasMaxLength(255);

        }
    }
}
