using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ReservationSystem.Areas.Admin.Models.Reservation
{
    public class Create
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime DateTime { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int Guests { get; set; }
        public string? Comments { get; set; }

        public SelectList ?ReservationStatus { get; set; }
        public int ReservationStatusId { get; set; }
        public SelectList ?ReservationOrigin { get; set; }
        public int ReservationOriginId { get; set; }

        public SelectList ?Sittings { get; set; }
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
