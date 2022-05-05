namespace ReservationSystem.Areas.Admin.Models
{
    public class Index
    {
        public int ResId { get; set; }
        public DateTime Date { get; set; }
        public string ReservationStatus { get; set; }
        public int SittingId { get; set; }
    }
}
