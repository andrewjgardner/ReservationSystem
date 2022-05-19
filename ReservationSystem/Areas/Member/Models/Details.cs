namespace ReservationSystem.Areas.Member.Models.Reservation
{
    public class Details
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Guests { get; set; }
        public string Status { get; set; }
    }
}
