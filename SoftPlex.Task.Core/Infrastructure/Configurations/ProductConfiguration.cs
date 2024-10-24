using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftPlex.Task.Core.Domain.Models;

namespace SoftPlex.Task.Core.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.HasKey(e => e.Id).HasName("PK__Product__3214EC275370E6CA");

            builder.ToTable("Product", tb => tb.HasTrigger("trg_Product_Audit"));

            builder.HasIndex(e => e.Name, "IX_Product_Name");

            builder.HasIndex(e => e.Name, "UC_Product_Name").IsUnique();

            builder.Property(e => e.Id)
                    .HasColumnName("ID");
            builder.Property(e => e.Name).HasMaxLength(255);

        }
    }
}
