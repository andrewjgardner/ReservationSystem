using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Areas.Admin.Models;
using ReservationSystem.Data;
using ReservationSystem.Models.Reservation;
using ReservationSystem.Services;

namespace ReservationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReservationController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly PersonService _personService;

        public ReservationController(ApplicationDbContext context, PersonService personService)
        {
            _personService = personService;
            _context = context;
        }




        public async Task<IActionResult> Index()
        {

            var m = new Models.Reservation.Index
            {
                Reservations = await _context.Reservations
                    .Include(r => r.ReservationStatus)
                    .Select(r => new Models.Reservation.Details
                    {
                        Id = r.Id,
                        DateTime = r.StartTime,
                        SittingId = r.SittingId,
                        Status = r.ReservationStatus.Description
                    })
                    .ToArrayAsync()
            };

            return View(m);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int reservationId)
        {
            var reservation = await _context.Reservations.Include(c=> c.Customer).FirstOrDefaultAsync(r => r.Id == reservationId);
            if(reservation == null)
            {
                return NotFound();
            }
            var reservationStatus = await _context.ReservationStatuses.ToListAsync();
            var reservationOrigin = await _context.ReservationOrigins.ToListAsync();

            var reservationEdit = new Models.Reservation.Edit
            {
              Id = reservation.Id,
              FirstName = reservation.Customer.FirstName,
              LastName = reservation.Customer.LastName,
              Email = reservation.Customer.Email,
              Phone = reservation.Customer.PhoneNumber,
              Date = reservation.StartTime,
              ReservationStatus = new SelectList(reservationStatus, "Id", "Description"),
              ReservationOrigin = new SelectList(reservationOrigin, "Id", "Description"),
              ReservationStatusId = reservation.ReservationStatusId,
              ReservationOriginId = reservation.ReservationOriginId,
              NumPeople = reservation.NoOfPeople
              
            };

            return View(reservationEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Models.Reservation.Edit m)
        {
            var reservation = await _context.Reservations.Where(r => r.Id == m.Id).Include(c => c.Customer).FirstOrDefaultAsync();
            var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == m.SittingId);
            if (reservation == null || sitting == null)
            {
                return NotFound();
            }

            m.Validate(ModelState, sitting);
            if (ModelState.IsValid)
            {

                try
                {
                    reservation.Customer.FirstName = m.FirstName;
                    reservation.Customer.LastName = m.LastName;
                    reservation.Customer.Email = m.Email;
                    reservation.Customer.PhoneNumber = m.Phone;
                    reservation.StartTime = m.Date;
                    reservation.ReservationOriginId = m.ReservationOriginId;
                    reservation.ReservationStatusId = m.ReservationStatusId;
                    reservation.NoOfPeople = m.NumPeople;

                    _context.Reservations.Update(reservation);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Error", e.Message);
                }
            }
            return View(m);

        }


    }
}
