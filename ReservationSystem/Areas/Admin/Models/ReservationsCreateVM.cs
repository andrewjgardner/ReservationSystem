using Microsoft.AspNetCore.Mvc.Rendering;

namespace ReservationSystem.Areas.Admin.Models
{
    public class ReservationsCreateVM
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int NumPeople { get; set; }
        public string Comments { get; set; }

        public SelectList ReservationStatus { get; set; }
        public int ReservationStatusId { get; set; }
        public SelectList ReservationOrigin { get; set; }
        public int ReservationOriginId { get; set; }

        public SelectList Sittings { get; set; }
        public int SittingId { get; set; }
    }
}
