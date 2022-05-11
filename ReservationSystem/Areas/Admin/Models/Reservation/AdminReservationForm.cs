using Microsoft.AspNetCore.Mvc.Rendering;

namespace ReservationSystem.Areas.Admin.Models.Reservation
{
    //Partial View Model
    public class AdminReservationForm
    {
        public SelectList? ReservationStatus { get; set; }
        public int ReservationStatusId { get; set; }

        public SelectList? ReservationOrigin { get; set; }
        public int ReservationOriginId { get; set; }

    }
}
