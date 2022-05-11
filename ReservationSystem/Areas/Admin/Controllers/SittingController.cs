﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Areas.Admin.Models;
using ReservationSystem.Data;

namespace ReservationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Employee, Manager")]
    public class SittingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly int _restaurantId;

        public SittingController(ApplicationDbContext context)
        {
            _context = context;
            _restaurantId = 1;
        }

        public async Task<IActionResult> Index()
        {
            var sittings = await _context.Sittings.Include(s => s.SittingType).Include(s => s.Reservations).ToArrayAsync();
            var m = new Models.Sitting.Index
            {
                Sittings = await _context.Sittings
                    .Include(s => s.SittingType)
                    .Select(s => new Summary
                    {
                        SittingID = s.Id,
                        Date = s.StartTime,
                        StartTime = s.StartTime,
                        EndTime = s.EndTime,
                        Title = s.Title,
                        PercentFull = s.PercentFull(),
                        IsClosed = s.IsClosed
                    })
                    .ToArrayAsync()
            };

            return View(m);

        }

        public async Task<IActionResult> Details(int sittingId)
        {
            var sitting = await _context.Sittings.Include(s => s.SittingType).Include(s => s.Reservations).ThenInclude(r => r.Customer).FirstOrDefaultAsync(s => s.Id == sittingId);
            var reservations = new List<Models.Sitting.ReservationList>();

            foreach (Reservation reservation in sitting.Reservations)
            {
                var reservationVM = new Models.Sitting.ReservationList
                {
                    StartTime = reservation.StartTime,
                    Name = reservation.Customer.FullName(),
                    Phone = reservation.Customer.PhoneNumber,
                    Comments = reservation.Comments,
                    Guests = reservation.Guests
                };
                reservations.Add(reservationVM);
            }

            var sittingVM = new Models.Sitting.Details
            {
                SittingId = sittingId,
                StartTime = sitting.StartTime,
                EndTime = sitting.EndTime,
                Title = sitting.Title,
                SittingType = sitting.SittingType.Description,
                ReservationList = reservations
            };
            return View(sittingVM);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == _restaurantId);

            var sittingtypes = await _context.SittingTypes.ToListAsync();

            var sitting = new Models.Sitting.Create
            {
                SittingTypes = new SelectList(sittingtypes, "Id", "Description"),
                RestaurantId = _restaurantId,
                Capacity = restaurant.DefaultCapacity,
                IsClosed = false
            };

            return View(sitting);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> Create(Models.Sitting.Create m)
        {
            var sittingtype = await _context.SittingTypes.FirstOrDefaultAsync(st => st.Id == m.SittingTypeId);
            var sittings = new List<Sitting>();
            for (int i = 0; i < m.NumberToSchedule; i++)
            {
                var sitting = new Sitting
                {
                    Title = m.Title,
                    StartTime = m.StartTime.AddDays(i),
                    EndTime = m.EndTime.AddDays(i),
                    Capacity = m.Capacity,
                    IsClosed = m.IsClosed,
                    SittingTypeId = m.SittingTypeId,
                    RestaurantId = m.RestaurantId,
                    SittingType = sittingtype,
                    ResDuration = sittingtype.ResDuration,
                };

                sittings.Add(sitting);
            }

            _context.Sittings.AddRange(sittings);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> Edit(int sittingId)
        {
            try
            {
                var sittingtypes = await _context.SittingTypes.ToListAsync();
                var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == sittingId);
                if (sitting == null)
                {
                    TempData["ErrorMessage"] = "Sitting not found";
                    return NotFound();
                }
                var m = new Areas.Admin.Models.Sitting.Edit
                {
                    Title = sitting.Title,
                    Date = sitting.StartTime.Date,
                    StartTime = sitting.StartTime,
                    EndTime = sitting.EndTime,
                    SittingTypes = new SelectList(sittingtypes, "Id", "Description"),
                    SittingTypeId = sitting.SittingTypeId,
                    SittingId = sitting.Id,
                    Capacity = sitting.Capacity,
                    RestaurantId = sitting.RestaurantId,
                    IsClosed = sitting.IsClosed
                };

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
        public async Task<IActionResult> Edit(Areas.Admin.Models.Sitting.Edit m)
        {
            try
            {
                var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == m.SittingId);
                if (sitting == null)
                {
                    TempData["ErrorMessage"] = "Sitting not found";
                    return NotFound();
                }

                var sittingType = await _context.SittingTypes.FirstOrDefaultAsync(st => st.Id == m.SittingTypeId);
                if (sittingType == null)
                {
                    TempData["ErrorMessage"] = "Sitting Type not found";
                    return NotFound();
                }

                m.Validate(ModelState, sitting);

                if (ModelState.IsValid)
                {
                    sitting.Title = m.Title;
                    sitting.StartTime = m.StartTime;
                    sitting.EndTime = m.EndTime;
                    sitting.Capacity = m.Capacity;
                    sitting.IsClosed = m.IsClosed;

                    _context.Update(sitting);

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.InnerException?.Message ?? ex.Message);
            }
            return View(m);
        }
    }
}
