﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class Car
    {
        [Key, MaxLength(14)]
        public string CarId { get; set; }
        public List<CarDriver> CarDrivers { get; set; }
    }
}