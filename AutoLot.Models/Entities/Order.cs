using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Models.Entities
{
    [Table("Orders", Schema ="dbo")]
    [EntityTypeConfiguration(typeof(OrderConfiguration))]
    public class Order : BaseEntity
    {
        [Column("InventoryId")]
        public int CarId { get; set; }
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CarId))]
        [InverseProperty(nameof(Car.Orders))]
        public Car CarNavigation { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty(nameof(Customer.Orders))]
        public virtual Customer CustomerNavigation { get; set; }
    }
}
