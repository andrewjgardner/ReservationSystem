using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data.Context;
using ReservationSystem.Services;
using System.Linq;

namespace ReservationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Employee, Manager")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly int _restaurantId;
        private UserService _userService;

        public UserController(ApplicationDbContext context, UserService userService)
        {
            _context = context;
            _restaurantId = 1;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var m = new Models.User.Index
            {
                Users = await _context.Users
                    .Select(u => new Models.User.Details
                    {
                        Id = u.Id,
                        Email = u.Email
                    })
                    .ToArrayAsync()
            };

            foreach (var user in m.Users)
            {
                user.Roles = await _userService.GetUserRolesAsync(user.Id);
            }

            return View(m);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var m = new Models.User.Create
            {

            };

            return View();
        }
    }
}
