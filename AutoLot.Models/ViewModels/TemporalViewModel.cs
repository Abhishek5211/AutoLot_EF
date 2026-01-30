using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Models.ViewModels
{
    public class TemporalViewModel<T> where T : class, new()
    {
        public T Entity { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

    }
}
