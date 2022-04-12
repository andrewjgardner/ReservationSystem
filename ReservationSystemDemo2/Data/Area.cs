namespace ReservationSystem.Data
{
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }

        public List<Table> Tables { get; set; } = new List<Table>();
    }
}
