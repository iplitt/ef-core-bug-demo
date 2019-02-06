using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data212
{
    public class Driver
    {
        [Key, MaxLength(14)]
        public string DriverId { get; set; }

        public List<CarDriver> CarDrivers { get; set; }
      
    }
}