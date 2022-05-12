using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Areas.Admin.Models.Sitting
{
    public class Close
    {
        public int SittingId { get; set; }

        public bool IsClosed { get; set; }

        public void Validate(ModelStateDictionary modelState, Data.Sitting sitting)
        {
        }
    }
}
