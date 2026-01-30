using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Models.Entities.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(d => d.CarNavigation)
                .WithMany(c => c.Orders)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Inventory");

            builder.HasOne(d => d.CustomerNavigation)
                .WithMany(p => p.Orders)
                .HasForeignKey(p => p.CustomerId)
                .HasConstraintName("FK_Orders_Customers");

            builder.ToTable(b => b.IsTemporal(t =>
            {
                t.HasPeriodEnd("ValidTo");
                t.HasPeriodStart("ValidFrom");
                t.UseHistoryTable("OrdersAudit");
            }));
        }
    }
}
