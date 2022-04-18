using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Models
{
    public class Drivers
    {
        [Key]
        public int DriverID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public int Titles { get; set; }

    }
}
