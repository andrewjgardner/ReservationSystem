using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models.Reservation
{
    public class Create
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public ReservationForm ReservationForm { get; set; }

        [HiddenInput]
        public int SittingId { get; set; }

    }
}
