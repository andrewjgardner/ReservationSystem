#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Models.Reservation;
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

        [HttpGet]
        public async Task<IActionResult> Sittings()
        {
            var sittings = GetSittings();
            List<SittingsVM> sittingsVM = sittings.Select(s => new SittingsVM
            {
                SittingID = s.Id,
                Date = s.StartTime,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Title = s.Title
            }).ToList();

            return View(sittingsVM);
        }

        [HttpGet]
        public async Task<IActionResult> ReservationForm(int sittingId)
        {
            var sittings = await _context.Sittings.Where(s => s.Id == sittingId).FirstOrDefaultAsync();

            if (sittings == null)
            {
                return NotFound();            
            }
            
            var reservationForm = new ReservationFormVM
            {
                Date = sittings.StartTime,
                SittingId = sittingId,
                StartTime = sittings.StartTime,
                EndTime = sittings.EndTime,
            };

            return View(reservationForm);
        }

        [HttpPost]
        public async Task<IActionResult> ReservationForm(ReservationFormVM reservationForm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var restaurantId = 1;
                    var sitting = await _context.Sittings.Where(s => s.Id == reservationForm.SittingId).FirstOrDefaultAsync();

                    if (sitting == null || sitting.IsClosed)
                    {
                        TempData["ErrorMessage"] = "Sitting is no longer available";
                        return RedirectToAction("Sittings");
                    }

                    var reservationstatus = await _context.ReservationStatuses.Where(rs => rs.Description == "Pending").FirstOrDefaultAsync();
                    var reservationorigin = await _context.ReservationOrigins.Where(ro => ro.Description == "Online").FirstOrDefaultAsync();

                    var customer = await _personService.FindOrCreateCustomerAsync(restaurantId, reservationForm.Phone, reservationForm.FirstName, reservationForm.LastName, reservationForm.Email);

                    DateTime arrival = reservationForm.Date.Date.Add(reservationForm.Time.TimeOfDay);

                    var reservation = new Reservation
                    {
                        StartTime = arrival,
                        NoOfPeople = reservationForm.NumPeople,
                        Comments = reservationForm.Comments,
                        SittingId = reservationForm.SittingId,
                        Sitting = sitting,
                        ReservationStatus = reservationstatus,
                        ReservationOrigin = reservationorigin,
                        Customer = customer
                    };

                    _context.Reservations.Add(reservation);

                    await _context.SaveChangesAsync();

                  
                    TempData["Id"] = reservation.Id;
                    TempData["ArrivalTime"] = reservation.StartTime;
                    TempData["NumberOfPeople"] = reservation.NoOfPeople;
                    TempData["Comments"] = reservation.Comments;

                    return RedirectToAction("Receipt");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.InnerException?.Message ?? ex.Message);
                }
            }

            return View(reservationForm);
        }

        [HttpGet]
        public async Task<IActionResult> Receipt()
        {
            try
            {
                var id = (int)TempData["Id"];
                if (id != 0)
                {
                    var receipt = new ReceiptVM
                    {
                        Id = id,
                        ArrivalTime = (DateTime)TempData["ArrivalTime"],
                        NumberOfPeople = (int)TempData["NumberOfPeople"],
                        Comments = (string)TempData["Comments"]
                    };

                    return View(receipt);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return NotFound();
            }

        }

    }
}
