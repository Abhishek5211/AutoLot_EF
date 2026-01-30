using AutoLot.Models.ViewModels.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Models.ViewModels
{
    [Keyless]
    [EntityTypeConfiguration(typeof(CustomerOrderViewModelConfiguration))]
    public class CustomerOrderViewModel
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string Color { get; set; }
        [Required]
        [StringLength(50)]
        public string NickName { get; set; }
        [Required]
        [StringLength(50)]
        public string Make { get; set; }
        public bool? IsDrivable { get; set; }
        public string Display { get; set; }
        [NotMapped]
        public string FullDetail => $"{FirstName} {LastName} ordered a {Color} {Make} named {NickName}";
        public override string ToString()
        {
            return FullDetail;
        }
}
}
