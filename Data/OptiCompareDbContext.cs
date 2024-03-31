using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
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

        public DbSet<Phone> phones { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Phone>(builder =>
            {
                builder.OwnsOne(e => e.batteryDetails);
                builder.OwnsOne(e => e.bodyDimensions);
                builder.OwnsOne(e => e.cameraDetails);
                builder.OwnsOne(e => e.displayDetails);
                builder.OwnsOne(e => e.platformDetails);
            });
            
        }
    }
}
