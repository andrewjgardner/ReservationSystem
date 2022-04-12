namespace ReservationSystem.Data
{
    public class Customer : Person
    {
        public int Id { get; set; }

        public Reservation Reservation { get; set; }
        public int ReservationId { get; set; }
    }
}
