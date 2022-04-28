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
                Title = s.Title,
                PercentFull = s.PercentFull()
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

        public async Task<IActionResult> SittingDetails(int sittingId)
        {
            var sitting = await _context.Sittings.Where(s => s.Id == sittingId).Include(s=>s.SittingType).Include(s=>s.Reservations).ThenInclude(r=>r.Customer).FirstOrDefaultAsync();
            var reservations = new List<ReservationListVM>();

            foreach (Reservation reservation in sitting.Reservations)
            {
                var reservationVM = new ReservationListVM
                {
                    StartTime = reservation.StartTime,
                    Name = reservation.Customer.FullName(),
                    Phone = reservation.Customer.PhoneNumber,
                    Comments = reservation.Comments
                };
                reservations.Add(reservationVM);
            }

            var sittingVM = new SittingDetailsVM
            {
                Date = sitting.StartTime.Date,
                StartTime = sitting.StartTime,
                EndTime = sitting.EndTime,
                Title = sitting.Title,
                SittingType = sitting.SittingType.Description,
                Reservations = reservations
            };
            return View(sittingVM);
        }
    }
}
