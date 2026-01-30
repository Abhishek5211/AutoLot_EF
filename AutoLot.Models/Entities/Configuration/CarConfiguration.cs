using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Models.Entities.Configuration
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasQueryFilter(c => c.IsDrivable);

            builder.Property(p => p.IsDrivable)
                .HasField("_isDrivable")
                .HasDefaultValue(true);

            builder.Property(p => p.DateBuilt).HasDefaultValueSql("getdate()");

            builder.Property(p => p.Display).HasComputedColumnSql("[NickName] + '(' + [Color] + ')'", true);

            CultureInfo provider = new CultureInfo("ne-NP");
            NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            builder.Property(p => p.Price)
                .HasConversion(
                v => decimal.Parse(v, style, provider),
                v => v.ToString("C2"));

            builder.HasMany(p => p.Drivers)
                .WithMany(p => p.Cars)
                .UsingEntity<CarDriver>(
                j => j
                .HasOne(cd => cd.DriverNavigation)
                .WithMany(d => d.CarDrivers)
                .HasForeignKey(nameof(CarDriver.DriverId))
                .HasConstraintName("FK_InventoryDriver_Drivers_DriverId")
                .OnDelete(DeleteBehavior.Cascade),
                j => j
                .HasOne(cd => cd.CarNavigation)
                .WithMany(c => c.CarDrivers)
                .HasForeignKey(nameof(CarDriver.CarId))
                .HasConstraintName("FK_InventoryDrivers_Inventory_InventoryId")
                .OnDelete(DeleteBehavior.ClientCascade),
                j =>
                {
                    j.HasKey(cd => new { cd.CarId, cd.DriverId });
                });


            builder.ToTable(b => b.IsTemporal(
                t =>
                {
                    t.HasPeriodStart("ValidFrom");
                    t.HasPeriodEnd("ValidTo");
                    t.UseHistoryTable("InventoryAudit");
                }));


        }
    }
}
