
namespace ReservationSystem.Data
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string? UserId { get; set; }

        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }

        public string FullName() { return FirstName + " " + LastName; }
    }

    public enum Roles
    {
        Admin,
        Employee,
        Member
    }
}
