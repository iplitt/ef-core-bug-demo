using System.ComponentModel.DataAnnotations;

namespace Data212
{
    public class CarDriver
    {
        [MaxLength(14)]
        public string CarId { get; set; }
        [MaxLength(14)]
        public string DriverId { get; set; }
    }
}