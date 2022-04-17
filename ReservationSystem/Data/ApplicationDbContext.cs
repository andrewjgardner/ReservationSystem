using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ReservationSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly Action<ModelBuilder> _dataConfigurer;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, Action<ModelBuilder> dataConfigurer = null )
            : base(options)
        {
            _dataConfigurer = dataConfigurer;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            _ = new ApplicationModelBuilder(builder);
            
            if (_dataConfigurer is not null)
            {
                _dataConfigurer(builder);
            }
            else
            {
                new DataSeeder(builder);
            }

            base.OnModelCreating(builder);
        }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationOrigin> ReservationOrigins { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Sitting> Sittings { get; set; }
        public DbSet<SittingType> SittingTypes { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<ReservationTable> ReservationTables { get; set; }

    }
}