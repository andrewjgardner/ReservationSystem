using ReservationSystem.Areas.Admin.Models.Reservation;
using ReservationSystem.Models.Reservation;

namespace ReservationSystem.Areas.Admin.Models.User
{
    internal class Create
    {
        public ReservationForm ReservationForm { get; set; }
        public AdminReservationForm AdminReservationForm { get; set; }
    }
}