namespace ReservationSystem.Models.Reservation
{
    public class ReceiptVM
    {
        public int Id { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int NumberOfPeople { get; set; }
        public string Comments { get; set; }
    }
}
