using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Models.Entities
{
    public class CarDriver : BaseEntity
    {

        [Column("InventoryId")]
        public int CarId { get; set; }
        public int DriverId { get; set; }

        [ForeignKey(nameof(CarId))]
        public virtual Car CarNavigation { set; get; }

        [ForeignKey(nameof(DriverId))]
        public virtual Driver DriverNavigation { get; set; }
    }
}
