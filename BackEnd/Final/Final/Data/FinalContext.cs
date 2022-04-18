using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Final.Models;

namespace Final.Data
{
    public class FinalContext : DbContext
    {
        public FinalContext (DbContextOptions<FinalContext> options)
            : base(options)
        {
        }

        public DbSet<Final.Models.Winners> Winners { get; set; }
        public DbSet<Final.Models.Drivers> Drivers { get; set; }
        public DbSet<Final.Models.Cars> Cars { get; set; }
        public DbSet<Final.Models.Tracks> Tracks { get; set; }

    }
}
