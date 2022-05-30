using Microsoft.AspNetCore.Mvc.Rendering;
using ReservationSystem.Models.Reservation;

namespace ReservationSystem.Areas.Admin.Models.Reservation
{
    public class AssignTables
    {
        public int ReservationId { get; set; }
        public List<Data.Table> ReservationTables { get; set; }
        public List<Data.Table> AllTables { get; set; }
    }
}
