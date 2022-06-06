#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Data.Context;
using ReservationSystem.Services;

namespace ReservationSystem.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PersonService _personService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly int _restaurantId;

        public ReservationController(ApplicationDbContext context, PersonService personService, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _personService = personService;
            _userManager = userManager;
            _restaurantId = 1;
        }

        public async Task<List<Sitting>> GetSittings()
        {
            //Currently, just shows all open sittings where the end time is in the future
            //We may want to make this more complex, e.g. by specifying that Sittings should be displayed if there is more than an hour left before the sitting ends
            //Also, sort by date, so that closer dates are first
            var sittings = await _context.Sittings.Where(s => s.RestaurantId == _restaurantId && s.EndTime > DateTime.Now).ToListAsync();
            return sittings;
        }

        public async Task<IActionResult> Sittings()
        {
            //This is going to be deleted anyway once we consolidate into a single view, no point refactoring
            var sittings = await GetSittings();
            List<Models.Reservation.Sittings> m = sittings.Select(s => new Models.Reservation.Sittings
            {
                SittingID = s.Id,
                Date = s.StartTime,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Title = s.Title
            }).ToList();

            return View(m);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int sittingId)
        {
            try
            {
                var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == sittingId);

                if (sitting == null)
                {
                    return NotFound();
                }

                var m = new Models.Reservation.Create
                {
                    ReservationForm = new Models.Reservation.ReservationForm()
                    {
                        DateTime = sitting.StartTime
                    },
                    SittingId = sittingId,
                    StartTime = sitting.StartTime,
                    EndTime = sitting.EndTime
                };

                if (User.Identity.IsAuthenticated && User.IsInRole(nameof(Roles.Member)))
                {
                    var user = await _userManager.GetUserAsync(User);
                    var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == user.Id);

                    m.ReservationForm.FirstName = customer.FirstName;
                    m.ReservationForm.LastName = customer.LastName;
                    m.ReservationForm.Phone = customer.PhoneNumber;
                    m.ReservationForm.Email = customer.Email;
                }
                return View(m);

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.InnerException?.Message ?? ex.Message;
                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.Reservation.Create m)
        {
            var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == m.SittingId);

            if (sitting == null)
            {
                return NotFound();
            }

            if (sitting?.IsClosed != false)
            {
                TempData["ErrorMessage"] = "Sitting is no longer available";
                return RedirectToAction("Sittings");
            }

            m.ReservationForm.Validate(ModelState, sitting);

            if (ModelState.IsValid)
            {
                try
                {
                    var reservationStatus = await _context.ReservationStatuses.Where(rs => rs.Description == "Pending").FirstOrDefaultAsync();
                    var reservationOrigin = await _context.ReservationOrigins.Where(ro => ro.Description == "Online").FirstOrDefaultAsync();

                    var customer = await _personService.FindOrCreateCustomerAsync(_restaurantId, m.ReservationForm.Phone, m.ReservationForm.FirstName, m.ReservationForm.LastName, m.ReservationForm.Email);
                    var reservation = new Reservation
                    {
                        StartTime = m.ReservationForm.DateTime,
                        Guests = m.ReservationForm.Guests,
                        Comments = m.ReservationForm.Comments,
                        SittingId = m.SittingId,
                        ReservationStatusId = reservationStatus.Id,
                        ReservationOriginId = reservationOrigin.Id,
                        Customer = customer
                    };

                    _context.Reservations.Add(reservation);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Receipt", new { reservationId = reservation.Id });

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.InnerException?.Message ?? ex.Message);
                }
            }

            m.StartTime = sitting.StartTime;
            m.EndTime = sitting.EndTime;
            return View(m);
        }

        [HttpGet]
        public async Task<IActionResult> Receipt(int reservationId)
        {
            try
            {
                var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId);
                if (reservation == null)
                {
                    return NotFound();
                }
                {
                    var receipt = new Models.Reservation.Receipt
                    {
                        Id = reservation.Id,
                        ArrivalTime = reservation.StartTime,
                        Guests = reservation.Guests,
                        Comments = reservation.Comments
                    };

                    return View(receipt);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.InnerException?.Message ?? ex.Message;
                return RedirectToAction("Exception", "Error");
            }

        }
    }
}
