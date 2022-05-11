using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Areas.Admin.Models.Reservation
{
    //Partial View Model
    public class AdminReservationForm
    {
        public SelectList? ReservationStatus { get; set; }
        [Display(Name = "Reservation Status")]
        public int ReservationStatusId { get; set; }

        public SelectList? ReservationOrigin { get; set; }
        [Display(Name = "Reservation Origin")]
        public int ReservationOriginId { get; set; }

    }
}
