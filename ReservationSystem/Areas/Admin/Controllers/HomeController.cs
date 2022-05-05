using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Areas.Admin.Models;
using ReservationSystem.Data;
using ReservationSystem.Models.Reservation;
using ReservationSystem.Services;

namespace ReservationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly PersonService _personService;

        public HomeController (ApplicationDbContext context, PersonService personService)
        {
            _personService = personService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Sittings()
        {
            var sittings = await _context.Sittings.Include(s => s.SittingType).Include(s=>s.Reservations).ToArrayAsync();
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

        public async Task<IActionResult> AddSitting()
        {
            //TODO: Get Restaurant from Employee ID
            int restaurantId = 1;

            var restaurant = await _context.Restaurants.Where(r => r.Id == restaurantId).FirstOrDefaultAsync();

            var sittingtypes = await _context.SittingTypes.ToListAsync();

            var sitting = new SittingsCreateVM
            {
                SittingTypes = new SelectList(sittingtypes, "Id", "Description"),
                RestaurantId = restaurantId,
                Capacity = restaurant.DefaultCapacity,
                IsClosed = false
            };

            return View(sitting);
        }

        public async Task<IActionResult> SittingDetails(int sittingId)
        {
            var sitting = await _context.Sittings.Where(s => s.Id == sittingId).Include(s=>s.SittingType).Include(s=>s.Reservations).ThenInclude(r=>r.Customer).FirstOrDefaultAsync();
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

            var sittingVM = new SittingDetailsVM
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

        public async Task<IActionResult> Reservations()
        {
            var reservation = await _context.Reservations.Include(r => r.ReservationStatus).ToArrayAsync();
            List<Models.Index> reservationsVm = reservation.Select(r => new Models.Index
            {
                ResId = r.Id,
                Date = r.StartTime,
                ReservationStatus = r.ReservationStatus.Description,
                SittingId = r.SittingId
            }).ToList();

            return View(reservationsVm);
        }

        public async Task<IActionResult> AddReservation(int? sittingId)
        {
            
            
            var reservationStatus = await _context.ReservationStatuses.ToListAsync();
            var reservationOrigin = await _context.ReservationOrigins.ToListAsync();

            var reservation = new ReservationsCreateVM
            {
                ReservationStatus = new SelectList(reservationStatus, "Id", "Description"),
                ReservationOrigin = new SelectList(reservationOrigin, "Id", "Description"),
            };
            if (sittingId.HasValue)
            {
                var sitting = await _context.Sittings.Where(s => s.Id == sittingId).FirstOrDefaultAsync();
                reservation.SittingId = (int)sittingId;
                reservation.StartTime = sitting.StartTime;
                reservation.EndTime = sitting.EndTime;
                reservation.Date = sitting.StartTime;
            }
            else
            {
                var sittings = await _context.Sittings.ToListAsync();
                reservation.Sittings = new SelectList(sittings, "Id", "StartTime");
            }
            return View(reservation);
        }

        public async Task<IActionResult> SaveReservation(ReservationsCreateVM reservationForm)
        {
            var sitting = await _context.Sittings.Where(s => s.Id == reservationForm.SittingId).FirstOrDefaultAsync();
            var restaruantId = 1;
            //var reservationstatus = await _context.ReservationStatuses.Where(rs => rs.Description == "Pending").FirstOrDefaultAsync();
            //var reservationorigin = await _context.ReservationOrigins.Where(ro => ro.Description == "Online").FirstOrDefaultAsync();

            var customer = await _personService.FindOrCreateCustomerAsync(restaruantId, reservationForm.Phone, reservationForm.FirstName, reservationForm.LastName, reservationForm.Email);

            string? comments = reservationForm.Comments;

            DateTime arrival = reservationForm.Date.Date.Add(reservationForm.Time.TimeOfDay);

            var reservation = new Reservation
            {
                StartTime = arrival,
                NoOfPeople = reservationForm.NumPeople,
                Comments = comments,
                SittingId = reservationForm.SittingId,
                ReservationStatusId = reservationForm.ReservationStatusId,
                ReservationOriginId = reservationForm.ReservationOriginId,
                Customer = customer
            };
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction("SittingDetails",new {sittingId = sitting.Id});
        }

        [HttpGet]
        public async Task<IActionResult> EditReservation(int reservationId)
        {
            var reservation = await _context.Reservations.Include(c=> c.Customer).FirstOrDefaultAsync(r => r.Id == reservationId);
            if(reservation == null)
            {
                return NotFound();
            }
            var reservationStatus = await _context.ReservationStatuses.ToListAsync();
            var reservationOrigin = await _context.ReservationOrigins.ToListAsync();

            var reservationEdit = new Edit
            {
              Id = reservation.Id,
              FirstName = reservation.Customer.FirstName,
              LastName = reservation.Customer.LastName,
              Email = reservation.Customer.Email,
              Phone = reservation.Customer.PhoneNumber,
              Date = reservation.StartTime,
              ReservationStatus = new SelectList(reservationStatus, "Id", "Description"),
              ReservationOrigin = new SelectList(reservationOrigin, "Id", "Description"),
              ReservationStatusId = reservation.ReservationStatusId,
              ReservationOriginId = reservation.ReservationOriginId,
              NumPeople = reservation.NoOfPeople
              
            };

            return View(reservationEdit);
        }

        [HttpPost]
        public async Task<IActionResult> EditReservation(Edit m)
        {
            var reservation = await _context.Reservations.Where(r => r.Id == m.Id).Include(c=> c.Customer).FirstOrDefaultAsync();
            var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == m.SittingId); 
            if(reservation == null || sitting == null)
            {
                return NotFound(); 
            }

            m.Validate(ModelState, sitting); 
            if(ModelState.IsValid)
            {

                try
                {
                    reservation.Customer.FirstName = m.FirstName;
                    reservation.Customer.LastName = m.LastName;
                    reservation.Customer.Email = m.Email;
                    reservation.Customer.PhoneNumber = m.Phone;
                    reservation.StartTime = m.Date;
                    reservation.ReservationOriginId = m.ReservationOriginId;
                    reservation.ReservationStatusId = m.ReservationStatusId;
                    reservation.NoOfPeople = m.NumPeople;

                    _context.Reservations.Update(reservation);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Reservations));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Error", e.Message);
                }
            }
            //if we get here the model was not valid so pass the model (including model state errors back to the view engine)
            return View(m); 
        }

        [HttpPost]
        public async Task<IActionResult> SaveSitting(SittingsCreateVM sittingform)
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
