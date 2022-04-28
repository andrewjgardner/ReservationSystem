using ReservationSystem.Data.Identity;

namespace ReservationSystem.Data
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public int? ApplicationUserId { get; set; }

        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }

        public string FullName() { return FirstName + " " + LastName; }
    }
}
