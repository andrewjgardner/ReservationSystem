using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Areas.Admin.Models;
using ReservationSystem.Data;
using ReservationSystem.Data.Context;
using ReservationSystem.Models.Reservation;
using ReservationSystem.Services;

namespace ReservationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Employee, Manager")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PersonService _personService;

        public HomeController(ApplicationDbContext context, PersonService personService)
        {
            _personService = personService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
