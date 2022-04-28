namespace ReservationSystem.Areas.Admin.Models
{
    public class ReservationListVM
    {
        public int ResId { get; set; }
        public DateTime Date { get; set; }
        public string ReservationStatus { get; set; }
        public int SittingId { get; set; }
    }
}
