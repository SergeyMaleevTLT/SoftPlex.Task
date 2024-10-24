using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftPlex.Task.Core.Domain.Models;

namespace SoftPlex.Task.Core.Infrastructure.Configurations
{
    public class ProductVersionConfiguration : IEntityTypeConfiguration<ProductVersion>
    {
        public void Configure(EntityTypeBuilder<ProductVersion> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__ProductV__3214EC27B06BB0E0");

            builder.ToTable("ProductVersion", tb => tb.HasTrigger("trg_ProductVersion_Audit"));

            builder.HasIndex(e => e.CreatingDate, "IX_ProductVersion_CreatingDate");

            builder.HasIndex(e => e.Height, "IX_ProductVersion_Height");

            builder.HasIndex(e => e.Length, "IX_ProductVersion_Length");

            builder.HasIndex(e => e.Name, "IX_ProductVersion_Name");

            builder.HasIndex(e => e.Width, "IX_ProductVersion_Width");

            builder.Property(e => e.Id)
                .HasColumnName("ID");
         
            builder.Property(e => e.Name).HasMaxLength(255);
            builder.Property(e => e.ProductId).HasColumnName("ProductID");

            builder.HasOne(d => d.Product).WithMany(p => p.ProductVersionSet)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductVersion_Product");
        }
    }
}
