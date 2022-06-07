using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private UserService _userService;
        private UserManager<IdentityUser> _userManager;

        public UserController(ApplicationDbContext context, UserService userService, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var m = new Models.User.Index
            {
                Users = await _context.Users
                    .Where(u=>u.LockoutEnd!=DateTime.MaxValue)
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
                Roles = new SelectList(await _userService.GetRolesAsync(), "Name", "Name")
            };

            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.User.Create m)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new IdentityUser
                    {
                        UserName = m.Email,
                        NormalizedUserName = _userManager.NormalizeName(m.Email),
                        Email = m.Email,
                        PhoneNumber = m.Phone,
                        NormalizedEmail = _userManager.NormalizeEmail(m.Email),
                        EmailConfirmed = true,
                        LockoutEnd = null
                    };
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, m.Password);
                    await _userManager.CreateAsync(user);
                    await _userManager.AddToRoleAsync(user, m.RoleName);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Error", ex.InnerException?.Message ?? ex.Message);
            }
            //If anything goes wrong, return View
            m.Roles = new SelectList(await _userService.GetRolesAsync(), "Name", "Name");
            return View(m);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var m = new Models.User.Edit
            {
                Id = user.Id,
                Roles = new SelectList(await _userService.GetRolesAsync(), "Name", "Name"),
                RoleName = (await _userManager.GetRolesAsync(user))[0],
                Email = user.Email,
                Phone = user.PhoneNumber
            };

            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Models.User.Edit m)
        {
            var user = await _userManager.FindByIdAsync(m.Id);

            user.UserName = m.Email;
            user.NormalizedUserName = _userManager.NormalizeName(m.Email);
            user.Email = m.Email;
            user.PhoneNumber = m.Phone;
            user.NormalizedEmail = _userManager.NormalizeEmail(m.Email);
            user.EmailConfirmed = true;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, m.Password);

            await _userManager.UpdateAsync(user);

            var oldrole = (await _userManager.GetRolesAsync(user))[0];
            if (oldrole != m.RoleName)
            {
                await _userManager.RemoveFromRoleAsync(user, oldrole);
                await _userManager.AddToRoleAsync(user, m.RoleName);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Remove(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            user.LockoutEnabled = true;
            user.LockoutEnd = DateTime.MaxValue;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }


    }
}
