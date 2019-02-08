using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data212
{
    public class Car
    {
        [MaxLength(14)]
        public string CarId { get; set; }
        public List<CarDriver> CarDrivers { get; set; }
    }
}