using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Areas.Admin.Models;
using ReservationSystem.Data;
using ReservationSystem.Data.Context;
using ReservationSystem.Models.Reservation;
using System.Web;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace ReservationSystem.Areas.Member.Controllers
{
    [Area("Member")]
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController (ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            int customerId = _context.People
                .Where(p => p.UserId == userId)
                .Select(p => p.Id)
                .First();

            DateTime today = DateTime.Today;

            var m = new Models.Reservation.Index
            {
                Reservations = await _context.Reservations
                    .Where(r => r.CustomerId == customerId)
                    .Include(r => r.ReservationStatus)
                    .Where(r => r.ReservationStatus.Id != 3)//Reservation status id 3 = cancelled, thus this filters out any cancelled reservations
                    .Where(r => r.StartTime > today)
                    .Select(r => new Models.Reservation.Details
                    {
                        Id = r.Id,
                        DateTime = r.StartTime,
                        Guests = r.Guests,
                        Status = r.ReservationStatus.Description
                    })
                    .ToArrayAsync()
            };

            return View(m);
        }
    }
}
