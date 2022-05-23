using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Areas.Admin.Models;
using ReservationSystem.Data;
using ReservationSystem.Data.Context;

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
                        PercentFull = s.PeopleBooked,
                        IsClosed = s.IsClosed
                    })
                    .OrderBy(s => s.StartTime)
                    .ToArrayAsync()
            };

            return View(m);

        }

        public async Task<IActionResult> Details(int sittingId)
        {
            var sitting = await _context.Sittings.Include(s => s.SittingType).Include(s => s.Reservations).ThenInclude(r => r.Customer).FirstOrDefaultAsync(s => s.Id == sittingId);

            if (sitting == null)
            {
                return NotFound();
            }

            try
            {
                var reservations = new List<Models.Sitting.ReservationListItem>();

                foreach (Reservation reservation in sitting.Reservations)
                {
                    var reservationVM = new Models.Sitting.ReservationListItem
                    {
                        Id = reservation.Id,
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
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.InnerException?.Message ?? ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        private SelectList getRecurringTypes()
        {
            return new SelectList(new List<string> { "Daily", "Weekly" });
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == _restaurantId);
            if (restaurant == null)
            {
                return NotFound();
            }

            try
            {
                var m = new Models.Sitting.Create
                {
                    SittingTypes = new SelectList(await _context.SittingTypes.ToListAsync(), "Id", "Description"),
                    RestaurantId = _restaurantId,
                    Capacity = restaurant.DefaultCapacity,
                    IsClosed = false,
                    RecurringTypes = getRecurringTypes(),
                    StartTime = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute),
                    EndTime = DateTime.Now.Date.AddHours(DateTime.Now.Hour + 4).AddMinutes(DateTime.Now.Minute)
                };

                return View(m);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.InnerException?.Message ?? ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        private Sitting createSitting(Models.Sitting.Create m, SittingType sittingtype)
        {
            return new Sitting
            {
                Title = m.Title,
                StartTime = m.StartTime,
                EndTime = m.EndTime,
                Capacity = m.Capacity,
                IsClosed = m.IsClosed,
                SittingTypeId = m.SittingTypeId,
                RestaurantId = m.RestaurantId,
                SittingType = sittingtype,
                ResDuration = sittingtype.ResDuration,
            };
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> Create(Models.Sitting.Create m)
        {
            m.Validate(ModelState);

            if (ModelState.IsValid)
            {
                try
                {
                    var sittingtype = await _context.SittingTypes.FirstOrDefaultAsync(st => st.Id == m.SittingTypeId);
                    if (m.IsRecurring)
                    {
                        var sittings = new List<Sitting>();
                        var firstStartTime = m.StartTime;
                        var firstEndTime = m.EndTime;
                        //Starts with day of first sitting, and loops over
                        //E.g. If the Sitting start time is Thursday, first loop will start with Thursday, and end with Wednesday
                        for (int i = 0; i < 7; i++)
                        {
                            int dayindex = (int)m.StartTime.DayOfWeek;
                            if (m.RecurringDays[dayindex])
                            {
                                for (int j = 0; j < m.NumberToSchedule; j++)
                                {
                                    var sitting = createSitting(m, sittingtype);

                                    sittings.Add(sitting);

                                    if (m.RecurringType == "Daily")
                                    {
                                        m.StartTime = m.StartTime.AddDays(1);
                                        m.EndTime = m.EndTime.AddDays(1);
                                    }
                                    else if (m.RecurringType == "Weekly")
                                    {
                                        m.StartTime = m.StartTime.AddDays(7);
                                        m.EndTime = m.EndTime.AddDays(7);
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("m.RecurringType", "Recurring Type can not be " + m.RecurringType);
                                    }
                                }
                            }
                            firstStartTime = firstStartTime.AddDays(1);
                            firstEndTime = firstEndTime.AddDays(1);
                            m.StartTime = firstStartTime;
                            m.EndTime = firstEndTime;
                        }
                        _context.Sittings.AddRange(sittings);

                    }
                    else
                    {
                        var sitting = createSitting(m, sittingtype);
                        _context.Sittings.Add(sitting);
                    }
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.InnerException?.Message ?? ex.Message);
                }
            }

            m.RecurringDays = new bool[7];
            m.RecurringTypes = getRecurringTypes();
            m.SittingTypes = new SelectList(await _context.SittingTypes.ToListAsync(), "Id", "Description");
            return View(m);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> Edit(int sittingId)
        {
            try
            {
                var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == sittingId);
                if (sitting == null)
                {
                    return NotFound();
                }

                var m = new Models.Sitting.Edit
                {
                    Title = sitting.Title,
                    Date = sitting.StartTime.Date,
                    StartTime = sitting.StartTime,
                    EndTime = sitting.EndTime,
                    SittingTypes = new SelectList(await _context.SittingTypes.ToListAsync(), "Id", "Description"),
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
                TempData["ErrorMessage"] = ex.InnerException?.Message ?? ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> Edit(Models.Sitting.Edit m)
        {
            try
            {
                var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == m.SittingId);
                if (sitting == null)
                {
                    return NotFound();
                }

                var sittingType = await _context.SittingTypes.FirstOrDefaultAsync(st => st.Id == m.SittingTypeId);
                if (sittingType == null)
                {
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

        [Authorize(Roles = "Manager")]
        [HttpGet] //id = sitting id
        public async Task<IActionResult> Close(int id)
        {
            try
            {
                var sittingtypes = await _context.SittingTypes.ToListAsync();
                var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == id);
                if (sitting == null)
                {
                    TempData["ErrorMessage"] = "Sitting not found";
                    return NotFound();
                }
                var m = new Areas.Admin.Models.Sitting.Close
                {
                    SittingId = sitting.Id,
                    IsClosed = sitting.IsClosed
                };

                return PartialView("_AdminCloseSittingPartial", m);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return NotFound();
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> Close(Areas.Admin.Models.Sitting.Close m)
        {
            try
            {
                var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == m.SittingId);
                if (sitting == null)
                {
                    TempData["ErrorMessage"] = "Sitting not found";
                    return NotFound();
                }

                m.Validate(ModelState, sitting);

                if (ModelState.IsValid)
                {
                    sitting.IsClosed = true;

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
