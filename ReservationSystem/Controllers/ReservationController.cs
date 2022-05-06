#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Services;

namespace ReservationSystem.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PersonService _personService;

        public ReservationController(ApplicationDbContext context, PersonService personService)
        {
            _context = context;
            _personService = personService;
        }

        public List<Sitting> GetSittings()
        {
            //Currently, just shows all open sittings where the end time is in the future
            //We may want to make this more complex, e.g. by specifying that Sittings should be displayed if there is more than an hour left before the sitting ends
            //Also, sort by date, so that closer dates are first
            return _context.Sittings.Where(s => !s.IsClosed).Where(s => s.EndTime > DateTime.Now).Include(s => s.SittingType).ToList();
        }

        public async Task<IActionResult> Sittings()
        {
            //This is going to be deleted anyway once we consolidate into a single view, no point refactoring
            var sittings = GetSittings();
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
            var sittings = await _context.Sittings.Where(s => s.Id == sittingId).FirstOrDefaultAsync();

            if (sittings == null)
            {
                return NotFound();
            }

            var m = new ReservationSystem.Models.Reservation.Create
            {
                Date = sittings.StartTime,
                SittingId = sittingId,
                StartTime = sittings.StartTime,
                EndTime = sittings.EndTime,
            };

            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationSystem.Models.Reservation.Create m)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var restaurantId = 1;
                    var sitting = await _context.Sittings.Where(s => s.Id == m.SittingId).FirstOrDefaultAsync();

                    if (sitting == null || sitting.IsClosed)
                    {
                        TempData["ErrorMessage"] = "Sitting is no longer available";
                        return RedirectToAction("Sittings");
                    }

                    var reservationstatus = await _context.ReservationStatuses.Where(rs => rs.Description == "Pending").FirstOrDefaultAsync();
                    var reservationorigin = await _context.ReservationOrigins.Where(ro => ro.Description == "Online").FirstOrDefaultAsync();

                    var customer = await _personService.FindOrCreateCustomerAsync(restaurantId, m.Phone, m.FirstName, m.LastName, m.Email);

                    DateTime arrival = m.Date.Date.Add(m.Time.TimeOfDay);

                    var reservation = new Reservation
                    {
                        StartTime = arrival,
                        NoOfPeople = m.NumPeople,
                        Comments = m.Comments,
                        SittingId = m.SittingId,
                        Sitting = sitting,
                        ReservationStatus = reservationstatus,
                        ReservationOrigin = reservationorigin,
                        Customer = customer
                    };

                    _context.Reservations.Add(reservation);

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Receipt", new {reservationId = reservation.Id});

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.InnerException?.Message ?? ex.Message);
                }
            }

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
                    var receipt = new ReservationSystem.Models.Reservation.Receipt
                    {
                        Id = reservation.Id,
                        ArrivalTime = reservation.StartTime,
                        NumberOfPeople = reservation.NoOfPeople, 
                        Comments = reservation.Comments
                    };

                    return View(receipt);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return NotFound();
            }

        }
    }
}
