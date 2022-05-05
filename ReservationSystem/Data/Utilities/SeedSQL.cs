using Microsoft.Data.SqlClient;
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

        public async Task SeedAll()
        {
            await DropTriggers();
            await SeedTriggers();
        }

        public async Task DropTriggers()
        {
            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQL","droptriggers.sql");
            string sql = await File.ReadAllTextAsync(file);
            await _context.Database.ExecuteSqlRawAsync(sql);
        }

        public async Task SeedTriggers()
        {
            foreach (string file in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQL","Triggers"), "*.sql").ToList())
            {
                string sql = await File.ReadAllTextAsync(file);
                await _context.Database.ExecuteSqlRawAsync(sql);
            }
        }

    }
}
