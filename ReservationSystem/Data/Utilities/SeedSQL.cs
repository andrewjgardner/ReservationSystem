using Microsoft.EntityFrameworkCore;

namespace ReservationSystem.Data.Utilities
{
    public class SeedSQL
    {
        private ApplicationDbContext _context;

        public SeedSQL(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedTriggers()
        {
            foreach (string file in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQL"), "*.sql").ToList())
            {
                string sql = File.ReadAllText(file);
                _context.Database.ExecuteSqlRaw(sql);
            }
        }

    }
}
