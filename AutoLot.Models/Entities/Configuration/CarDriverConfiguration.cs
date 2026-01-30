using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Models.Entities.Configuration
{
    public class CarDriverConfiguration : IEntityTypeConfiguration<CarDriver>
    {
        public void Configure(EntityTypeBuilder<CarDriver> builder)
        {
            builder.HasQueryFilter(cd => cd.CarNavigation.IsDrivable);

            builder.ToTable(b => b.IsTemporal(t =>
            {
                t.HasPeriodEnd("ValidTo");
                t.HasPeriodStart("ValidFrom");
                t.UseHistoryTable("InventoryToDriversAudit");
            }
               ));
        }
    }
}
