namespace ReservationSystem.Areas.Admin.Models.Reservation
{
    public class Details
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
        public int SittingId { get; set; }
        public List<Data.Table> AllTables { get; set; }
    }
}
