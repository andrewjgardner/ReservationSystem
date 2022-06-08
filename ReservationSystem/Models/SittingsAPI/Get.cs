namespace ReservationSystem.Models.SittingsAPI
{
    public class Get
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int resDuration { get; set; }

    }
}
