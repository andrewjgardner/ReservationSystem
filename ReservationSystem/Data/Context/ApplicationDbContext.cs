using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data.Utilities;

namespace ReservationSystem.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            _ = new ApplicationModelBuilder(builder);
            var dataSeeder = new DataSeeder(builder);
            dataSeeder.SeedIdentity();
            dataSeeder.Seed();

            base.OnModelCreating(builder);
        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationOrigin> ReservationOrigins { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Sitting> Sittings { get; set; }
        public DbSet<SittingType> SittingTypes { get; set; }
        public DbSet<Table> Tables { get; set; }

        //public DbSet<ReservationTable> ReservationTables { get; set; }
    }
}
