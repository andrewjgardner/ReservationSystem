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
                Date = s.StartTime.ToShortDateString(),
                StartTime = s.StartTime.ToShortTimeString(),
                EndTime = s.EndTime.ToShortTimeString(),
                Title = s.Title
            }).ToList();

            return View(sittingsVM);
        }

    }
}
