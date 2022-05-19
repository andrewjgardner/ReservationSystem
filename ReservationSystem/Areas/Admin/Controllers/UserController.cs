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

        public UserController(ApplicationDbContext context)
        {

            _context = context;
            _restaurantId = 1;
        }

        public List<string> GetUserRoles(string userid)
        {
            var roles = _context.UserRoles
                .Where(ur => ur.UserId == userid)
                .Join(_context.Roles,
                    ur => ur.RoleId,
                    r => r.Id,
                    (ur, r) => r.Name
                )
                .ToList();
            return roles;
        }

        public async Task<IActionResult> Index()
        {
            var m = new Models.User.Index
            {
                Users = await _context.Users
                    .Select(u => new Models.User.Details
                    {
                        Id = u.Id,
                        Email = u.Email,
                        Roles = GetUserRoles(u.Id)
                    })
                    .ToArrayAsync()
            };

            return View(m);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
