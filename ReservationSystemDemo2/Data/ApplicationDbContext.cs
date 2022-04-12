using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ReservationSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<Area>().HasOne(a => a.Restaurant).WithMany(r => r.Areas).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Person>().HasOne(p => p.Restaurant).WithMany(r => r.People).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Reservation>().HasOne(r => r.Sitting).WithMany(s => s.Reservations).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Reservation>().HasOne(r => r.ReservationOrigin).WithMany(ro=>ro.Reservations).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Reservation>().HasOne(r => r.ReservationStatus).WithMany(rs=>rs.Reservations).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Reservation>().HasOne(r => r.Customer).WithMany(c => c.Reservations).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Sitting>().HasOne(s => s.Restaurant).WithMany(r => r.Sittings).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Sitting>().HasOne(s => s.SittingType).WithMany(st=>st.Sittings).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Table>().HasOne(a => a.Area).WithMany(a => a.Tables).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Reservation>()
                .HasMany(r => r.Tables).WithMany(t => t.Reservations)
                .UsingEntity<Dictionary<string, object>>(
                    "ReservationTables",
                    rt => rt.HasOne<Table>().WithMany().OnDelete(DeleteBehavior.Restrict),
                    rt => rt.HasOne<Reservation>().WithMany().OnDelete(DeleteBehavior.Restrict));

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
    }
}