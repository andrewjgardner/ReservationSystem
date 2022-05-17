using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data.Utilities;

namespace ReservationSystem.Data.Context
{
    public class TestingDbContext : ApplicationDbContext
    {
        public TestingDbContext(DbContextOptions<TestingDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            _ = new ApplicationModelBuilder(builder);
            base.OnModelCreating(builder);
        }
    }
}
