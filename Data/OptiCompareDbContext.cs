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

        public DbSet<Phone> Phones { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Phone>(builder =>
            {
                builder.OwnsOne(e => e.BatteryDetails);
                builder.OwnsOne(e => e.BodyDimensions);
                builder.OwnsOne(e => e.CameraDetails);
                builder.OwnsOne(e => e.DisplayDetails);
                builder.OwnsOne(e => e.PlatformDetails);
            });
            
        }
    }
}
