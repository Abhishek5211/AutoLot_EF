using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Models.Entities.Configuration
{
    public class CreditRiskConfiguration : IEntityTypeConfiguration<CreditRisk>
    {
        public void Configure(EntityTypeBuilder<CreditRisk> builder)
        {
            builder.HasOne(d => d.CustomerNavigation)
                .WithMany(p => p.CreditRisks)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CreditRisks_Customers");

            builder.OwnsOne(pd => pd.PersonalInformation, pc =>
            {
                pc.Property<string>(nameof(Person.FirstName))
                .HasColumnName(nameof(Person.FirstName))
                .HasColumnType("nvarchar(50");
                pc.Property<string>(nameof(Person.LastName))
                    .HasColumnName(nameof(Person.LastName))
                    .HasColumnType("nvarchar(50)");
                pc.Property(p => p.FullName)
                     .HasColumnName(nameof(Person.FullName))
                     .HasComputedColumnSql("[LastName] + ', ' + [FirstName]");
            });
            builder.Navigation(d => d.PersonalInformation).IsRequired(true);
        

  
        }
    }
}
