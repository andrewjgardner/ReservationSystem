namespace ReservationSystem.Data
{
    public class Customer : Person
    {
        public string Email { get; set; }


        public List<Reservation> Reservations = new List<Reservation>();
    }
}
