using Microsoft.EntityFrameworkCore;

namespace Data212
{
    public class DemoContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<CarDriver> CarDrivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarDriver>()
                .HasKey(r => new { r.CarId, r.DriverId });
        }
    }
}
