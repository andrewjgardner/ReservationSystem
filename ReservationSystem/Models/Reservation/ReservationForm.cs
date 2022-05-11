using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models.Reservation
{
    public class ReservationForm
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Display(Name = "Date")]
        public DateTime DateTime { get; set; }

        [Range(1,8)]
        [Required]
        [Display(Name = "Number of guests")]
        public int Guests { get; set; }

        public string? Comments { get; set; }

        public void Validate(ModelStateDictionary modelState, Data.Sitting sitting)
        {

            if (DateTime < sitting.StartTime || DateTime > sitting.EndTime)
            {
                modelState.AddModelError("ReservationForm.DateTime", "Reservation Time Must Fall Within Sitting");
            }
        }


    }
}
