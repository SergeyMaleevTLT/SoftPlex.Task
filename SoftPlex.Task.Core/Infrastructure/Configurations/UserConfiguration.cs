using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftPlex.Task.Core.Domain.Models;

namespace SoftPlex.Task.Core.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__AuthUser__3214EC272E31F89D");
        
        builder.Property(e => e.Id)
            .HasColumnName("ID");
            
        builder.HasIndex(e => e.Login, "IX_AuthUser_Login");
        
        builder.ToTable("AuthUser");
    }
}