using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data212
{
    public class Driver
    {
        [Key, MaxLength(14)]
        public string DriverId { get; set; }
        [MaxLength(14)]
        public string Name { get; set; }

        public List<CarDriver> CarDrivers { get; set; }
      
    }
}