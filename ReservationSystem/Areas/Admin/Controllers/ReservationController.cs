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
                    AdminReservationForm = new Models.Reservation.AdminReservationForm
                    {
                        ReservationStatus = new SelectList(await _context.ReservationStatuses.ToListAsync(), "Id", "Description"),
                        ReservationOrigin = new SelectList(await _context.ReservationOrigins.ToListAsync(), "Id", "Description")
                    },
                };

                if (sittingId.HasValue)
                {
                    var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == sittingId);
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
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.InnerException?.Message ?? ex.Message);
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
            try
            {
                var reservation = await _context.Reservations.Include(c => c.Customer).FirstOrDefaultAsync(r => r.Id == reservationId);
                if (reservation == null)
                {
                    return NotFound();
                }

                var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == reservation.SittingId);
                if (sitting == null)
                {
                    return NotFound();
                }

                var m = new Models.Reservation.Edit
                {
                    ReservationForm = new ReservationSystem.Models.Reservation.ReservationForm
                    {
                        FirstName = reservation.Customer.FirstName,
                        LastName = reservation.Customer.LastName,
                        Email = reservation.Customer.Email,
                        Phone = reservation.Customer.PhoneNumber,
                        DateTime = reservation.StartTime,
                        Guests = reservation.Guests,
                        Comments = reservation.Comments
                    },
                    AdminReservationForm = new Models.Reservation.AdminReservationForm
                    {
                        ReservationStatus = new SelectList(await _context.ReservationStatuses.ToListAsync(), "Id", "Description"),
                        ReservationOrigin = new SelectList(await _context.ReservationOrigins.ToListAsync(), "Id", "Description"),
                        ReservationStatusId = reservation.ReservationStatusId,
                        ReservationOriginId = reservation.ReservationOriginId
                    },
                    SittingId = reservation.SittingId,
                    Id = reservation.Id,
                    StartTime = sitting.StartTime,
                    EndTime = sitting.EndTime
                };

                return View(m);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.InnerException?.Message ?? ex.Message;
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Models.Reservation.Edit m)
        {
            var reservation = await _context.Reservations.Include(c => c.Customer).FirstOrDefaultAsync(r => r.Id == m.Id);
            var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == m.SittingId);
            if (reservation == null || sitting == null)
            {
                return NotFound();
            }

            m.ReservationForm.Validate(ModelState, sitting);

            if (ModelState.IsValid)
            {
                try
                {
                    reservation.Customer.FirstName = m.ReservationForm.FirstName;
                    reservation.Customer.LastName = m.ReservationForm.LastName;
                    reservation.Customer.Email = m.ReservationForm.Email;
                    reservation.Customer.PhoneNumber = m.ReservationForm.Phone;
                    reservation.Guests = m.ReservationForm.Guests;
                    reservation.StartTime = m.ReservationForm.DateTime;
                    reservation.Comments = m.ReservationForm.Comments;
                    reservation.ReservationOriginId = m.AdminReservationForm.ReservationOriginId;
                    reservation.ReservationStatusId = m.AdminReservationForm.ReservationStatusId;

                    _context.Reservations.Update(reservation);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Error", e.Message);
                }
            }

            m.AdminReservationForm.ReservationStatus = new SelectList(await _context.ReservationStatuses.ToListAsync(), "Id", "Description");
            m.AdminReservationForm.ReservationOrigin = new SelectList(await _context.ReservationOrigins.ToListAsync(), "Id", "Description");

            return View(m);
        }
    }
}
