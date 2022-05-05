using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Areas.Admin.Models.Sitting
{
    public class Details
    {
        [DisplayFormat(DataFormatString = "{0:dddd dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:t}")]
        [Display(Name = "Start time")]
        public DateTime StartTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:t}")]
        [Display(Name = "End time")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Name")]
        public string Title { get; set; }

        [Display(Name = "Sitting Type")]
        public string SittingType { get; set; }

        public List<SittingReservationListVM> Reservations { get; set; }
        public int SittingId { get; set; }
    }

    public class SittingReservationListVM
    {
        [DisplayFormat(DataFormatString = "{0:t}")]
        [Display(Name = "Arrival time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Customer Name")]
        public string Name { get; set; }

        [Display(Name = "Customer Phone")]
        public string Phone { get; set; }

        [Display(Name = "Number of People")]
        public int NumPeople { get; set; }

        [Display(Name = "Comments")]
        public string? Comments { get; set; }
    }
}
