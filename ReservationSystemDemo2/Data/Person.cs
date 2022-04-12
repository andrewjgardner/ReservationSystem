namespace ReservationSystem.Data
{
    public class Person
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }
    }
}
