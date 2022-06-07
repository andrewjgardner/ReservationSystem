namespace ReservationSystem.Models.UserAPI
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int Guests { get; set; }
        public string Comments { get; set; }
        public string Name { get; set; }
    }
}
