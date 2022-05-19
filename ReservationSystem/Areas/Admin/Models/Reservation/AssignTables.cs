using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Areas.Admin.Models.Reservation
{
    public class AssignTables
    {
        public int ReservationId { get; set; }

        public List<Data.Table> ReservationTables { get; set; }

        public List<Data.Table> AllTables { get; set; }

        public void Validate(ModelStateDictionary modelState, Data.Sitting sitting)
        {
        }
    }
}
