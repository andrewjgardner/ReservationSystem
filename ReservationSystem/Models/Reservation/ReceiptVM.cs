using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models.Reservation
{
    public class ReceiptVM
    {
        public int Id { get; set; }
        
        [Display( Name = "Arrival Time" )]
        public DateTime ArrivalTime { get; set; }

        [Display(Name = "Guests")]
        public int Guests { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }
    }
}
