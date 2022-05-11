using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ReservationSystem.Areas.Admin.Models.Reservation
{
    public class Create
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ReservationSystem.Models.Reservation.ReservationForm ReservationForm { get; set; }
        public AdminReservationForm AdminReservationForm { get; set; }

        public SelectList? Sittings { get; set; }
        public int SittingId { get; set; }
    }
}
