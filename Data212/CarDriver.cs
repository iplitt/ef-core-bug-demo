using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data212
{
    public class CarDriver
    {
        [Key, Column(Order = 1), MaxLength(14)]
        public string CarId { get; set; }
        [Key, Column(Order = 2), MaxLength(14)]
        public string DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}