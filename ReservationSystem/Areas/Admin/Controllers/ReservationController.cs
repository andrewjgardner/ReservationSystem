using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Data.Context;
using ReservationSystem.Services;
using System.Diagnostics;
using System.Text.RegularExpressions;

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
            Trace.Listeners.Add(new TextWriterTraceListener("MyOutput.log"));
            Debug.WriteLine("Debugging has started in reservation index");
            Debug.WriteLine("Checking if Reservations are in index");
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
            Debug.Assert(m is { }, "reservations is null");
            Debug.WriteLineIf(m is null, "Reservations is null");
            Debug.WriteLineIf(m is { }, $"number of reservations = {m.Reservations.Length}");
            Debug.WriteLine("End of reservation index debugging");
            Trace.Close();
            return View(m);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
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
            m.ReservationForm.Validate(ModelState, sitting);

            if (ModelState.IsValid)
            {
                try
                {
                    var customer = await _personService.FindOrCreateCustomerAsync(_restaurantId, m.ReservationForm.FirstName, m.ReservationForm.LastName, m.ReservationForm.Email, m.ReservationForm.Phone);
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
                return RedirectToAction("Exception", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Models.Reservation.Edit m)
        {
            Trace.Listeners.Add(new TextWriterTraceListener("MyOutput.log"));
            Debug.WriteLine("Debugging in Edit Post request.");
            Debug.WriteLine("Checking to make sure no errors occur");
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
                    Debug.Assert(reservation.Id == m.Id, "Id does not match");
                    Debug.WriteLineIf(reservation.Id == m.Id, $"Id does match, id is {m.Id}");
                    Debug.Assert(Regex.IsMatch(reservation.Customer.Email, "^\\S+@\\S+$", RegexOptions.IgnoreCase), "Email entered does meet email format");
                    Debug.WriteLineIf(Regex.IsMatch(reservation.Customer.Email, "^\\S+@\\S+$", RegexOptions.IgnoreCase), "Email entered does meet email format.");
                    Debug.WriteLine("End of reservation Edit post request debugging");
                    Trace.Close();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Error", e.Message);
                }
            }

            m.AdminReservationForm.ReservationStatus = new SelectList(await _context.ReservationStatuses.ToListAsync(), "Id", "Description");
            m.AdminReservationForm.ReservationOrigin = new SelectList(await _context.ReservationOrigins.ToListAsync(), "Id", "Description");
            m.StartTime = sitting.StartTime;
            m.EndTime = sitting.EndTime;

            return View(m);
        }
    }
}
