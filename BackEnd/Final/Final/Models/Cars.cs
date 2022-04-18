using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Models
{
    public class Cars
    {
       [Key]
        public int CarID { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int Year { get; set; }
    }
}
