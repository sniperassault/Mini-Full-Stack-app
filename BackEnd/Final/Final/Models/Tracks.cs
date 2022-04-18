using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Models
{
    public class Tracks
    {
        [Key]
        public int TrackID { get; set; }
        public string TrackName { get; set; }
        public string Location { get; set; }
        public int Length { get; set; }
        public int Capacity { get; set; }
        public int Turns { get; set; }

    }
}
