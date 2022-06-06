using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data.Context;
using ReservationSystem.Services;

namespace ReservationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Manager")]
    public class ReportController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly PersonService _personService;
        private readonly int _restaurantId;

        public ReportController(ApplicationDbContext context, PersonService personService)
        {
            _context = context;
            _personService = personService;
            _restaurantId = 1;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> SittingData()
        {
            var data = await _context.Sittings
                .GroupBy(s => new { s.StartTime.Year, s.StartTime.Month })
                .Select(r => new
                {
                    PeopleBooked = r.Sum(s => s.PeopleBooked),
                    Month = new DateTime(r.Key.Year, r.Key.Month, 1)
                })
                .ToArrayAsync();

            return new JsonResult(data);

        }
    }
}
