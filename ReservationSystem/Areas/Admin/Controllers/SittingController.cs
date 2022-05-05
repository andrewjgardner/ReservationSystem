using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Areas.Admin.Models;
using ReservationSystem.Data;

namespace ReservationSystem.Areas.Admin.Controllers
{
    public class SittingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SittingController(ApplicationDbContext context)
        {
            _context = context;
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
                        PercentFull = s.PercentFull()
                    })
                    .ToArrayAsync()
            };

            List<SittingsListVM> sittingsVM = sittings.Select(s => new SittingsListVM
            {
                SittingID = s.Id,
                Date = s.StartTime,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Title = s.Title,
                PercentFull = s.PercentFull()
            }).ToList();

            return View(sittingsVM);

        }

        public async Task<IActionResult> SittingDetails(int sittingId)
        {
            var sitting = await _context.Sittings.Where(s => s.Id == sittingId).Include(s => s.SittingType).Include(s => s.Reservations).ThenInclude(r => r.Customer).FirstOrDefaultAsync();
            var reservations = new List<SittingReservationListVM>();

            foreach (Reservation reservation in sitting.Reservations)
            {
                var reservationVM = new SittingReservationListVM
                {
                    StartTime = reservation.StartTime,
                    Name = reservation.Customer.FullName(),
                    Phone = reservation.Customer.PhoneNumber,
                    Comments = reservation.Comments,
                    NumPeople = reservation.NoOfPeople
                };
                reservations.Add(reservationVM);
            }

            var sittingVM = new Details
            {
                SittingId = sittingId,
                Date = sitting.StartTime.Date,
                StartTime = sitting.StartTime,
                EndTime = sitting.EndTime,
                Title = sitting.Title,
                SittingType = sitting.SittingType.Description,
                Reservations = reservations
            };
            return View(sittingVM);
        }

        [HttpPost]
        public async Task<IActionResult> SaveSitting(Create sittingform)
        {
            var sittingtype = await _context.SittingTypes.Where(st => st.Id == sittingform.SittingTypeId).FirstOrDefaultAsync();
            var sitting = new Sitting
            {
                Title = sittingform.Title,
                StartTime = sittingform.StartTime,
                EndTime = sittingform.EndTime,
                Capacity = sittingform.Capacity,
                IsClosed = sittingform.IsClosed,
                SittingTypeId = sittingform.SittingTypeId,
                RestaurantId = sittingform.RestaurantId,
                SittingType = sittingtype,
                ResDuration = sittingtype.ResDuration
            };

            _context.Sittings.Add(sitting);
            await _context.SaveChangesAsync();


            return RedirectToAction("Sittings");
        }

    }
}
