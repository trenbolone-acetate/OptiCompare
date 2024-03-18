using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OptiCompare.Models;

namespace OptiCompare.Data
{
    public class OptiCompareDbContext : DbContext
    {
        public OptiCompareDbContext (DbContextOptions<OptiCompareDbContext> options)
            : base(options)
        {
        }

        public DbSet<Phone> Phone { get; set; } = default!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=opticompare-db;user=root;password=A5r4t3e2m1;");
        }
    }
}
