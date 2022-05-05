using Microsoft.EntityFrameworkCore;

namespace ReservationSystem.Data
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int Guests { get; set; }
        
        public string? Comments { get; set; }

        public Sitting Sitting { get; set; }
        public int SittingId { get; set; }

        public ReservationStatus ReservationStatus { get; set; }
        public int ReservationStatusId { get; set; }

        public ReservationOrigin ReservationOrigin { get; set; }
        public int ReservationOriginId { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public List<Table> Tables { get; set; } = new List<Table>();

        /*
        public IEnumerable<Table> Tables(ApplicationDbContext context)
        {
            var reservationtables = context.ReservationTables.Where(rt => rt.ReservationId == this.Id).Include(rt => rt.Table).ToList();
            var tables = reservationtables.Select(rt => rt.Table);
            return tables;
        }
        */
    }
}
