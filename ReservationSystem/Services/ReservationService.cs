using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Data.Context;

namespace ReservationSystem.Services
{
    public class ReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<Table>> GetTablesAsync(int reservationId)
        //{
        //    var reservationtables = await _context.ReservationTables.Where(rt => rt.ReservationId == reservationId).Include(rt => rt.Table).ToListAsync();
        //    var tables = reservationtables.Select(rt => rt.Table);
        //    return tables;
        //}
    }
}
