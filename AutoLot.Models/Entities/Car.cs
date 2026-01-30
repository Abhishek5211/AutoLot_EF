using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Models.Entities
{
    [Table("Inventory", Schema = "dbo")]
    [Index(nameof(MakeId), Name = "IX_Inventory_MakeId")]
    [EntityTypeConfiguration(typeof(CarConfiguration))]
    public class Car : BaseEntity
    {
        [Required, DisplayName("Make")]
        public int MakeId { get; set; }

        [Required, StringLength(50)]
        public string Color { get; set; }
        public string Price { get; set; }

        [Required, StringLength(50)]
        [DisplayName("Nick Name")]
        public string NickName { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string Display { get; set; }


        public DateTime? DateBuilt { get; set; }

        [ForeignKey(nameof(MakeId))]
        [InverseProperty(nameof(Make.Cars))]
        public virtual Make MakeNavigation { get; set; }

        [InverseProperty(nameof(Order.CarNavigation))]
        public virtual IEnumerable<Order> Orders { get; set; } = new List<Order>();

        [InverseProperty(nameof(Driver.Cars))]
        public virtual IEnumerable<Driver> Drivers { get; set; } = new List<Driver>();

        [InverseProperty(nameof(CarDriver.CarNavigation))]
        public virtual IEnumerable<CarDriver> CarDrivers { get; set; } = new List<CarDriver>();

        [InverseProperty(nameof(Radio.CarNavigation))]
        public virtual Radio RadioNavigation { get; set; }


        private bool? _isDrivable;

        [DisplayName("Is Drivable")]
        public bool IsDrivable { get => _isDrivable ?? true; set => _isDrivable = value; }
        [NotMapped]
        public string MakeName => MakeNavigation?.Name ?? "Unknown";


        public override string ToString()
        {
            return $"{NickName ?? "No Name"} is a {Color} {MakeName} with Id {Id}.";
        }


    }
}
