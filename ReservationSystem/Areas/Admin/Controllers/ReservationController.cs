using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Services;

namespace ReservationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Employee, Manager")]
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PersonService _personService;
        private readonly int _restaurantId;

        public ReservationController(ApplicationDbContext context, PersonService personService)
        {
            _personService = personService;
            _context = context;
            _restaurantId = 1;
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

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> Create(int? sittingId)
        {
            try
            {
                var m = new Models.Reservation.Create
                {
                    ReservationForm = new ReservationSystem.Models.Reservation.ReservationForm(),
                    AdminReservationForm = new Models.Reservation.AdminReservationForm(),
                };

                m.AdminReservationForm.ReservationStatus = new SelectList(await _context.ReservationOrigins.ToListAsync(), "Id", "Description");
                m.AdminReservationForm.ReservationOrigin = new SelectList(await _context.ReservationStatuses.ToListAsync(), "Id", "Description");

                if (sittingId.HasValue)
                {
                    var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == sittingId);
                    m.SittingId = (int)sittingId;
                    m.SittingId = (int)sittingId;
                    m.StartTime = sitting.StartTime;
                    m.EndTime = sitting.EndTime;
                    m.ReservationForm.DateTime = sitting.StartTime;
                }
                else
                {
                    var sittings = await _context.Sittings.ToListAsync();
                    m.Sittings = new SelectList(sittings, "Id", "StartTime");
                }
                return View(m);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return NotFound();
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> Create(Models.Reservation.Create m)
        {
            var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == m.SittingId);
            if (sitting == null)
            {
                return NotFound();
            }
            m.ReservationForm.Validate(ModelState, sitting);

            if (ModelState.IsValid)
            {
                try
                {
                    var customer = await _personService.FindOrCreateCustomerAsync(_restaurantId, m.ReservationForm.Phone, m.ReservationForm.FirstName, m.ReservationForm.LastName, m.ReservationForm.Email);
                    var reservation = new Reservation
                    {
                        StartTime = m.ReservationForm.DateTime,
                        Guests = m.ReservationForm.Guests,
                        Comments = m.ReservationForm.Comments,
                        SittingId = m.SittingId,
                        ReservationStatusId = m.AdminReservationForm.ReservationStatusId,
                        ReservationOriginId = m.AdminReservationForm.ReservationOriginId,
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

            m.AdminReservationForm.ReservationStatus = new SelectList(await _context.ReservationStatuses.ToListAsync(), "Id", "Description", m.AdminReservationForm.ReservationStatusId);
            m.AdminReservationForm.ReservationOrigin = new SelectList(await _context.ReservationOrigins.ToListAsync(), "Id", "Description", m.AdminReservationForm.ReservationOriginId);
            m.StartTime = sitting.StartTime;
            m.EndTime = sitting.EndTime;
            m.SittingId = sitting.Id;

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
        public async Task<IActionResult> Edit(Models.Reservation.Edit m)
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
