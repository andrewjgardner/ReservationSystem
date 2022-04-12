namespace ReservationSystem.Data
{
    public class Customer : Person
    {
        public int Id { get; set; }

        public List<Reservation> Reservations { get; set; }
        public int ReservationId { get; set; }
    }
}
