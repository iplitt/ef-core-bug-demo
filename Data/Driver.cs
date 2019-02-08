using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class Driver
    {
        [MaxLength(14)]
        public string DriverId { get; set; }

        public List<CarDriver> CarDrivers { get; set; }
      
    }
}