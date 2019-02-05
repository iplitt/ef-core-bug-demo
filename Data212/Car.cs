using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data212
{
    public class Car
    {
        [Key, MaxLength(14)]
        public string CarId { get; set; }
        [Required, MaxLength(14)]
        public string Model { get; set; }
        public List<CarDriver> CarDrivers { get; set; }
    }
}