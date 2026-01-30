using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AutoLot.Models.Entities
{
    [Table("Customers",Schema ="dbo")]
    [EntityTypeConfiguration(typeof(CustomerConfiguration))]
    public class Customer : BaseEntity
    {

        public Person PersonalInformation { get; set; } = new Person();

        [InverseProperty(nameof(CreditRisk.CustomerNavigation))]
        public virtual IEnumerable<CreditRisk> CreditRisks { get; set; } = new List<CreditRisk>();

        [InverseProperty(nameof(Order.CustomerNavigation))]
        public virtual IEnumerable<Order> Orders { get; set; } = new List<Order>();
    }
}
