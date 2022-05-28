namespace ReservationSystem.Models.UserAPI
{
    public class Reservation
    {
        public DateTime StartTime { get; set; }
        public int Guests { get; set; }
        public string Comments { get; set; }
    }
}
