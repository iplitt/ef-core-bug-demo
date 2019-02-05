using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Data
{
    public class CarDriver
    {
        [Key, Column(Order = 1), MaxLength(14)]
        public string CarId { get; set; }
        [Key, Column(Order = 2), MaxLength(14)]
        public string DriverId { get; set; }

        [JsonIgnore]
        public Driver Driver { get; set; }
    }
}