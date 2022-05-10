using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Areas.Admin.Models.Sitting
{
    public class Edit
    {
        [DisplayFormat(DataFormatString = "{0:dddd dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:t}")]
        [Display(Name = "Start time")]
        public DateTime StartTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:t}")]
        [Display(Name = "End time")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Name")]
        public string Title { get; set; }

        [Display(Name = "Sitting Type")]
        public SelectList? SittingTypes { get; set; }
        public int SittingTypeId { get; set; }

        public int SittingId { get; set; }

        public int RestaurantId { get; set; }

        public int Capacity { get; set; }

        public bool IsClosed { get; set; }

        public void Validate(ModelStateDictionary modelState, Data.Sitting sitting)
        {
        }

    }
}
