using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Areas.Admin.Models.Sitting
{
    public class AssignTables
    {
        public int SittingId { get; set; }

        public List<Data.Table> Tables { get; set; }

        public void Validate(ModelStateDictionary modelState, Data.Sitting sitting)
        {
        }
    }
}
