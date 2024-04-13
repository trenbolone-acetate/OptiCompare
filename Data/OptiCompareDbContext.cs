using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OptiCompare.Models;
using OptiCompare.PhoneSpecs;

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
                    builder.HasOne(p => p.bodyDimensions)
                        .WithOne(e => e.Phone)
                        .HasForeignKey<BodyDimensions>(b => b.PhoneId);
                    
                    builder.HasOne(p => p.displayDetails)
                        .WithOne(e => e.Phone)
                        .HasForeignKey<DisplayDetails>(b => b.PhoneId);
                    
                    builder.HasOne(p => p.platformDetails)
                        .WithOne(e => e.Phone)
                        .HasForeignKey<PlatformDetails>(b => b.PhoneId);
                    
                    builder.HasOne(p => p.cameraDetails)
                        .WithOne(e => e.Phone)
                        .HasForeignKey<CameraDetails>(b => b.PhoneId);

                    builder.HasOne(p => p.batteryDetails)
                        .WithOne(e => e.Phone)
                        .HasForeignKey<BatteryDetails>(b => b.PhoneId);
            });
            
        }
    }
}
