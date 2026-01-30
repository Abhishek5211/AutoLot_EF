using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Models.Entities
{
    [Table("Drivers", Schema = "dbo")]
    [EntityTypeConfiguration(typeof(DriverConfiguration))]
    public class Driver : BaseEntity
    {
        public Person PersonalInformation { get; set; } = new Person();

        [InverseProperty(nameof(Car.Drivers))]
        public virtual IEnumerable<Car> Cars { get; set; } = new List<Car>();

        [InverseProperty(nameof(CarDriver.DriverNavigation))]
        public virtual IEnumerable<CarDriver> CarDrivers { get; set; } = new List<CarDriver>();
    }
}
