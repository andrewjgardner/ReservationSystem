using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Areas.Admin.Models;
using ReservationSystem.Areas.Admin.Models.Reservation;
using ReservationSystem.Data;
using ReservationSystem.Models.Reservation;
using ReservationSystem.Services;

namespace ReservationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Employee, Manager")]
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PersonService _personService;

        public ReservationController(ApplicationDbContext context, PersonService personService)
        {
            _personService = personService;
            _context = context;
        }

        [HttpGet]
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

        [Authorize(Roles="Manager")]
        [HttpGet]
        public async Task<IActionResult> Create(int? sittingId)
        {
            var reservationStatus = await _context.ReservationStatuses.ToListAsync();
            var reservationOrigin = await _context.ReservationOrigins.ToListAsync();

            var reservation = new Models.Reservation.Create
            {
                ReservationStatus = new SelectList(reservationStatus, "Id", "Description"),
                ReservationOrigin = new SelectList(reservationOrigin, "Id", "Description"),
            };
            if (sittingId.HasValue)
            {
                var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == sittingId);
                reservation.SittingId = (int)sittingId;
                reservation.StartTime = sitting.StartTime;
                reservation.EndTime = sitting.EndTime;
                reservation.DateTime = sitting.StartTime;
            }
            else
            {
                var sittings = await _context.Sittings.ToListAsync();
                reservation.Sittings = new SelectList(sittings, "Id", "StartTime");
            }
            return View(reservation);
        }

        [Authorize(Roles="Manager")]
        [HttpPost]
        public async Task<IActionResult> Create(Models.Reservation.Create m)
        {
            var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == m.SittingId);
            var restaruantId = 1;
            //var reservationstatus = await _context.ReservationStatuses.Where(rs => rs.Description == "Pending").FirstOrDefaultAsync();
            //var reservationorigin = await _context.ReservationOrigins.Where(ro => ro.Description == "Online").FirstOrDefaultAsync();

            var customer = await _personService.FindOrCreateCustomerAsync(restaruantId, m.Phone, m.FirstName, m.LastName, m.Email);

            //Removes comments from the ModelState, preventing validation on the comments field, allowing empty fields to pass, then saving any passed values to the database
            //Kind of ugly, but it works
            string? comments = m.Comments;
            ModelState.Remove("Comments");

            if (sitting == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var reservation = new Reservation
                    {
                        StartTime = m.DateTime,
                        Guests = m.Guests,
                        Comments = comments,
                        SittingId = m.SittingId,
                        ReservationStatusId = m.ReservationStatusId,
                        ReservationOriginId = m.ReservationOriginId,
                        Customer = customer
                    };
                    _context.Reservations.Add(reservation);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Details", "Sitting", new { sittingId = sitting.Id });
                }
                catch (Exception e)
                {

                    ModelState.AddModelError("Error", e.Message);
                }
            }
            return View(m);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int reservationId)
        {
            var reservation = await _context.Reservations.Include(c => c.Customer).FirstOrDefaultAsync(r => r.Id == reservationId);
            if (reservation == null)
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
              DateTime = reservation.StartTime,
              ReservationStatus = new SelectList(reservationStatus, "Id", "Description"),
              ReservationOrigin = new SelectList(reservationOrigin, "Id", "Description"),
              ReservationStatusId = reservation.ReservationStatusId,
              ReservationOriginId = reservation.ReservationOriginId,
              Guests = reservation.Guests,
              SittingId = reservation.SittingId,
              Comments = reservation.Comments
              
            };

            return View(reservationEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Edit m)
        {
            var reservationStatus = await _context.ReservationStatuses.ToListAsync();
            var reservationOrigin = await _context.ReservationOrigins.ToListAsync();

            var reservation = await _context.Reservations.Include(c => c.Customer).FirstOrDefaultAsync(r => r.Id == m.Id);
            var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == m.SittingId);
            if (reservation == null || sitting == null)
            {
                return NotFound();
            }

            m.ReservationStatus = new SelectList(reservationStatus, "Id", "Description");
            m.ReservationOrigin = new SelectList(reservationOrigin, "Id", "Description");

            //Removes comments from the ModelState, preventing validation on the comments field, allowing empty fields to pass, then saving any passed values to the database
            //Kind of ugly, but it works
            string? comments = m.Comments;
            ModelState.Remove("Comments");

            m.Validate(ModelState, sitting);
            if (ModelState.IsValid)
            {
                try
                {
                    reservation.Customer.FirstName = m.FirstName;
                    reservation.Customer.LastName = m.LastName;
                    reservation.Customer.Email = m.Email;
                    reservation.Customer.PhoneNumber = m.Phone;
                    reservation.StartTime = m.DateTime;
                    reservation.ReservationOriginId = m.ReservationOriginId;
                    reservation.ReservationStatusId = m.ReservationStatusId;
                    reservation.Guests = m.Guests;
                    reservation.Comments = m.Comments;

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
