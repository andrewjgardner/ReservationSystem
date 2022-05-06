using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models.Reservation
{

    public class Create
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime Time { get; set; }

        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }

        [Required]
        public int NumPeople { get; set; }

        [Required]
        public string Comments { get; set; }

        public int SittingId { get; set; }
        
    }
}
