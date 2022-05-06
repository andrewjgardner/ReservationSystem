using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models.Reservation
{
    public class Sittings
    {
        public int SittingID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dddd dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:t}")]
        [Display(Name = "Start time")]
        public DateTime StartTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:t}")]
        [Display(Name = "End time")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Name")]
        public string? Title { get; set; }

    }
}
