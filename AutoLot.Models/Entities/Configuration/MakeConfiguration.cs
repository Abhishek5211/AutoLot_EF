using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Models.Entities.Configuration
{
    public class MakeConfiguration : IEntityTypeConfiguration<Make>
    {
        public void Configure(EntityTypeBuilder<Make> builder)
        {
            builder.ToTable(b => b.IsTemporal(t =>
            {
                t.HasPeriodEnd("ValidTo");
                t.HasPeriodStart("ValidFrom");
                t.UseHistoryTable("MakesAudit");
            }));
        }
    }
}
