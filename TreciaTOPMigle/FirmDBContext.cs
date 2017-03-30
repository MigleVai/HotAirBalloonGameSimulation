using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreciaTOPMigle
{
    class FirmDBContext : DbContext
    {
        public FirmDBContext() : base()
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Balloon> Balloons { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Player> Players { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Balloon>()
                        .Property(c => c.BalloonId)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Pilot>()
                        .HasOptional(s => s.Balloon)
                        .WithRequired(ad => ad.Pilot);

        }
    }
}
