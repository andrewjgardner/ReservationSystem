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

        public List<Data.Sitting> GetSittings()
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
            List<Models.Reservation.SittingVM> sittingsVM = sittings.Select(s => new Models.Reservation.SittingVM
            {
                SittingID = s.Id,
                Date = s.StartTime,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Title = s.Title
            }).ToList();

            return View(sittingsVM);
        }

        public async Task<IActionResult> Form(int sittingId)
        {
            var sittings =  await _context.Sittings.Where(s => s.Id == sittingId).FirstOrDefaultAsync();

            //TODO: Validate Sitting ID to check it's open and in future

            var reservationForm = new Form
            {
                Date = sittings.StartTime,
                SittingId = sittingId,
                StartTime = sittings.StartTime,
                EndTime = sittings.EndTime,
            };
               
            return View(reservationForm);
        }

        public async Task<IActionResult> Receipt(Form reservationForm)
        {
            var restaurantId = 1; 
            var sitting = await _context.Sittings.Where(s => s.Id == reservationForm.SittingId).FirstOrDefaultAsync();
            var reservationstatus = await _context.ReservationStatuses.Where(rs => rs.Description == "Pending").FirstOrDefaultAsync();
            var reservationorigin = await _context.ReservationOrigins.Where(ro => ro.Description == "Online").FirstOrDefaultAsync();
            var customer = await _personService.FindOrCreateCustomerAsync(restaurantId,reservationForm.Phone,reservationForm.FirstName, reservationForm.LastName, reservationForm.Email);

            string? comments = reservationForm.Comments;

            DateTime arrival = reservationForm.Date.Date.Add(reservationForm.Time.TimeOfDay);

            var reservation = new Reservation
            {
                StartTime = arrival,
                Guests = reservationForm.Guests,
                Comments = comments,
                SittingId = reservationForm.SittingId,
                Sitting = sitting,
                ReservationStatus = reservationstatus,
                ReservationOrigin = reservationorigin,
                Customer = (Customer)customer
            };

            _context.Reservations.Add(reservation);

            await _context.SaveChangesAsync();

            var model = new Receipt
            {
                Id = reservation.Id,
                ArrivalTime = reservation.StartTime,
                Guests = reservation.Guests,
                Comments = reservation.Comments
            };

            return View(model);

        }

    }
}
