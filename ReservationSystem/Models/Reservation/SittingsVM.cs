using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models.Reservation
{
    public class SittingsVM
    {
        public int SittingID { get; set; }
        public string? Date { get; set; }

        [Display(Name = "Start time")]
        public string? StartTime { get; set; }

        [Display(Name = "End time")]
        public string? EndTime { get; set; }

        [Display(Name = "Name")]
        public string? Title { get; set; }

    }
}
