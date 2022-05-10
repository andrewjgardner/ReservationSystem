using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReservationSystem.Data;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Areas.Admin.Models.Reservation
{
    public class Edit
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public int Guests { get; set; }
        [Required(AllowEmptyStrings=true)]
        public string? Comments { get; set; }

        public SelectList ?ReservationStatus { get; set; }
        [Required]
        public int ReservationStatusId { get; set; }
        public SelectList ?ReservationOrigin { get; set; }
        [Required]
        public int ReservationOriginId { get; set; }

        public SelectList ?Sittings { get; set; }
        [Required]
        public int SittingId { get; set; }


        public void Validate(ModelStateDictionary modelState, Data.Sitting sitting)
        {
            
            if (DateTime < sitting.StartTime || DateTime > sitting.EndTime)
            {
                modelState.AddModelError("Time", "Reservation Time Must Fall Within Sitting"); 
            }
        }

    }
}
