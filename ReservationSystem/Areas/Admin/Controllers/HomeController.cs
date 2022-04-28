using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Areas.Admin.Models;
using ReservationSystem.Data;
using ReservationSystem.Models.Reservation;

namespace ReservationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public HomeController (ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Sittings()
        {
            var sittings = await _context.Sittings.Include(s => s.SittingType).ToArrayAsync();
            List<SittingsListVM> sittingsVM = sittings.Select(s => new SittingsListVM
            {
                SittingID = s.Id,
                Date = s.StartTime,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Title = s.Title
            }).ToList();

            return View(sittingsVM);
        }

        public async Task<IActionResult> AddSitting()
        {
            //TODO: Get Restaurant from Employee ID
            int restaurantId = 1;

            var sittingtypes = await _context.SittingTypes.ToListAsync();

            var sitting = new SittingsCreateVM
            {
                SittingTypes = new SelectList(sittingtypes, "Id", "Description"),
                RestaurantId = restaurantId
            };

            return View(sitting);
        }

        public async Task<IActionResult> Reservations()
        {
            var reservation = await _context.Reservations.Include(r => r.ReservationStatus).ToArrayAsync();
            List<ReservationListVM> reservationsVm = reservation.Select(r => new ReservationListVM
            {
                ResId = r.Id,
                Date = r.StartTime,
                ReservationStatus = reservation.Select(r => r.ReservationStatus.Description).Where()
                SittingId = r.SittingId
            }).ToList();

            return View(reservationsVm);
        }
    }
}
