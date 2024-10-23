using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftPlex.Task.Core.Data.Models;


namespace  SoftPlex.Task.Core.Data.Configurations
{
    public class EventLogConfiguration : IEntityTypeConfiguration<EventLog>
    {
        public void Configure(EntityTypeBuilder<EventLog> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__EventLog__3214EC272E31F89D");

            builder.HasIndex(e => e.EventDate, "IX_EventLog_EventDate");

            builder.Property(e => e.Id)
                    .HasColumnName("ID");
        }
    }
}
