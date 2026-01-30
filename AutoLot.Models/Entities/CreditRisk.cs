using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AutoLot.Models.Entities
{
    [Table("CreditRisks", Schema = "dbo")]
    [EntityTypeConfiguration(typeof(CreditRiskConfiguration))]

    public class CreditRisk : BaseEntity
    {
        public int CustomerId { get; set; }

        public Person PersonalInformation { get; set; } = new Person();

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty(nameof(Customer.CreditRisks))]
        public virtual Customer CustomerNavigation { get; set; }
    }
}
