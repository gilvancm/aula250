using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc247.Models;

namespace SalesWebMvc247.Data
{
    public class SalesWebMvc247Context : DbContext
    {
        public SalesWebMvc247Context (DbContextOptions<SalesWebMvc247Context> options)
            : base(options)
        {
        }

        public DbSet<SalesWebMvc247.Models.Department> Department { get; set; }
        public DbSet<SalesWebMvc247.Models.Seller> Seller { get; set; }
        public DbSet<SalesWebMvc247.Models.SalesRecord> SalesRecord { get; set; }




    }
}
