using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Areas.Admin.Models.Sitting
{
    public class Create
    {

        //[DisplayFormat(DataFormatString = "{0:dddd dd/MM/yyyy}")]
        //public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:t}")]
        [Display(Name = "Start time")]
        public DateTime StartTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:t}")]
        [Display(Name = "End time")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Name")]
        public string Title { get; set; }

        public SelectList ?SittingTypes { get; set; }

        public int SittingTypeId { get; set; }

        public int RestaurantId { get; set; }

        public int Capacity { get; set; }

        public bool IsClosed { get; set; }

        //Recurring

        public bool IsRecurring { get; set; }

        public SelectList? RecurringTypes { get; set; }

        public string? RecurringType { get; set; }

        public int? NumberToSchedule { get; set; }

        public bool[] RecurringDays { get; set; } = new bool[7];

        public void Validate(ModelStateDictionary modelState)
        {
            if (StartTime > EndTime)
            {
                modelState.AddModelError("Create.EndTime", "End Time must be after Start Time");
            }
            else if (EndTime < DateTime.Now)
            {
                modelState.AddModelError("Create.EndTime", "Sitting can not be in the past");
            }
            if (IsRecurring)
            {
                if (RecurringType == null)
                {
                    modelState.AddModelError("Create.RecurringType", "Recurring Type can not be null");
                }
                if (NumberToSchedule <= 0 || NumberToSchedule == null)
                {
                    modelState.AddModelError("Create.NumberToSchedule", "Recurring Type must be a positive integer");
                }

                if (RecurringType=="Weekly")
                {
                    if (RecurringDays.Length != 7)
                    {
                        modelState.AddModelError("Create.RecurringDays", "RecurringDays must have one bool for each day of the week.");
                    }

                    bool atLeastOneDaySet = false;

                    foreach (bool b in RecurringDays)
                    {
                        if (b)
                        {
                            atLeastOneDaySet = true;
                            break;
                        }
                    }
                    if (!atLeastOneDaySet)
                    {
                        modelState.AddModelError("Create.RecurringDays", "At least one day must be set.");
                    }
                }
            }
        }
    }
}
