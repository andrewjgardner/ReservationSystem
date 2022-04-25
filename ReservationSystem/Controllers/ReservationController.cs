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

namespace ReservationSystem.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Sittings()
        {
            var sittings = await _context.Sittings.Include(s => s.SittingType).ToArrayAsync();
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

        public async Task<IActionResult> ReservationForm(int sittingId)
        {
            var sittings =  await _context.Sittings.Where(s => s.Id == sittingId).FirstOrDefaultAsync();

            var reservationForm = new ReservationFormVM
            {
                Date = sittings.StartTime,
                SittingId = sittingId,
                StartTime = sittings.StartTime,
                EndTime = sittings.EndTime,
            };
               
            return View(reservationForm);
        }

        public async Task<IActionResult> Receipt(ReservationFormVM reservationForm)
        {
            var sitting = await _context.Sittings.Where(s => s.Id == reservationForm.SittingId).FirstOrDefaultAsync();
            var reservationstatus = await _context.ReservationStatuses.Where(rs => rs.Description == "Pending").FirstOrDefaultAsync();
            var reservationorigin = await _context.ReservationOrigins.Where(ro => ro.Description == "Online").FirstOrDefaultAsync();
            var customer = await _context.Customers.Where(c => c.PhoneNumber == reservationForm.Phone).FirstOrDefaultAsync();

            if (customer == null)
            {
                var names = reservationForm.Name.Split(' ');
                if (names.Length==1)
                {
                    //Temporary hack
                    //TODO: Change reservation form to have separate first and last name entries
                    names = new string[] { names[0], "Anonymous" };
                }
                //TODO: Make it so that the restaurant is tracked through the control flow
                customer = new Customer
                {
                    Email = reservationForm.Email,
                    PhoneNumber = reservationForm.Phone,
                    FirstName = names[0],
                    LastName = names[1],
                    RestaurantId = 1
                };
            }

            var reservation = new Reservation
            {
                StartTime = reservationForm.Time,
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

            var model = new ReceiptVM
            {
                Id = reservation.Id,
                ArrivalTime = reservation.StartTime,
                NumberOfPeople = reservation.NoOfPeople,
                Comments = reservation.Comments
            };

            return View(model);

        }

    }
}
