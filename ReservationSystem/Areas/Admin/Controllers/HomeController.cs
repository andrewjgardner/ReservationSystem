﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Areas.Admin.Models;
using ReservationSystem.Data;
using ReservationSystem.Models.Reservation;

namespace ReservationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public HomeController (ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Sittings()
        {
            var sittings = await _context.Sittings.Include(s => s.SittingType).ToArrayAsync();
            List<SittingsListVM> sittingsVM = sittings.Select(s => new SittingsListVM
            {
                SittingID = s.Id,
                Date = s.StartTime,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Title = s.Title
            }).ToList();

            return View(sittingsVM);
        }

        public async Task<IActionResult> AddSitting()
        {
            //TODO: Get Restaurant from Employee ID
            int restaurantId = 1;

            var sittingtypes = await _context.SittingTypes.ToListAsync();

            var sitting = new SittingsCreateVM
            {
                SittingTypes = new SelectList(sittingtypes, "Id", "Description"),
                RestaurantId = restaurantId
            };

            return View(sitting);
        }

        public async Task<IActionResult> Reservations()
        {
            var reservation = await _context.Reservations.Include(r => r.ReservationStatus).ToArrayAsync();
            List<ReservationListVM> reservationsVm = reservation.Select(r => new ReservationListVM
            {
                ResId = r.Id,
                Date = r.StartTime,
                ReservationStatus = r.ReservationStatus.Description,
                SittingId = r.SittingId
            }).ToList();

            return View(reservationsVm);
        }
        public async Task<IActionResult> AddReservation()
        {
            var sittings = await _context.Sittings.ToListAsync();
            var reservationStatus = await _context.ReservationStatuses.ToListAsync();
            var reservationOrigin = await _context.ReservationOrigins.ToListAsync();

            var reservation = new ReservationsCreateVM
            {
                Sittings = new SelectList(sittings,"Id","StartTime"),
                ReservationStatus = new SelectList(reservationStatus, "Id", "Description"),
                ReservationOrigin = new SelectList(reservationOrigin, "Id", "Description"),
            };

            return View(reservation);
        }

        public async Task<IActionResult>SaveReservation(ReservationsCreateVM reservationForm)
        {
            var restaruantId = 1;
            var customer = await _context.Customers.Where(c => c.PhoneNumber == reservationForm.Phone).FirstOrDefaultAsync();
            //var reservationstatus = await _context.ReservationStatuses.Where(rs => rs.Description == "Pending").FirstOrDefaultAsync();
            //var reservationorigin = await _context.ReservationOrigins.Where(ro => ro.Description == "Online").FirstOrDefaultAsync();

            if (customer == null)
            {
                customer = new Customer
                {
                    RestaurantId = restaruantId,
                    Email = reservationForm.Email,
                    PhoneNumber = reservationForm.Phone,
                    FirstName = reservationForm.FirstName,
                    LastName = reservationForm.LastName
                };
            }

            string? comments = reservationForm.Comments;

            if (comments == null)
            {
                comments = "";
            }

            var reservation = new Reservation
            {
                StartTime = reservationForm.StartTime,
                NoOfPeople = reservationForm.NumPeople,
                Comments = comments,
                SittingId = reservationForm.SittingId,
                ReservationStatusId = reservationForm.ReservationStatusId,
                ReservationOriginId = reservationForm.ReservationOriginId,
                Customer = customer
            };
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction("Reservations");
        }

    }
}
