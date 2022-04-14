namespace ReservationSystem.Data
{
    public class ReservationTable
    {
        public Reservation Reservation { get; set; }
        public int ReservationId { get; set; }

        public Table Table { get; set; }
        public int TableId { get; set; }
    }
}
