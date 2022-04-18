using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Models
{
    public class Winners
    {
        [Key]
        public int WinnerID { get; set; }
        public Drivers Driver { get; set; }
        public Cars Car { get; set; }
        public Tracks Track { get; set; }
        public DateTime Date { get; set; }
    }
}
