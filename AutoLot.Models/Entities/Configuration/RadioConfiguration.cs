using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Models.Entities.Configuration
{
    public class RadioConfiguration : IEntityTypeConfiguration<Radio>
    {
        public void Configure(EntityTypeBuilder<Radio> builder)
        {
            builder.HasQueryFilter(r => r.CarNavigation.IsDrivable);
            builder.HasIndex(e => e.CarId, "IX_Radios_CarId").IsUnique();
            builder.HasOne(r => r.CarNavigation)
                .WithOne(c => c.RadioNavigation)
                .HasForeignKey<Radio>(d => d.CarId);

            builder.ToTable(b => b.IsTemporal(t =>
            {
                t.HasPeriodEnd("ValidTo");
                t.HasPeriodStart("ValidFrom");
                t.UseHistoryTable("RadiosAudit");
            }));
        }
    }
}
