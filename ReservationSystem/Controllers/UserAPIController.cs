using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Data.Context;
using ReservationSystem.Models;
using System.Security.Claims;

namespace ReservationSystem.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserAPIController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet("me")]
        public async Task<UserData> GetCurrentUser()
        {
            IdentityUser? user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return new UserData();
            }

            return new UserData
            {
                Authenticated = true,
                Email = user.Email
            };
        }

        [HttpGet("roles")]
        public HashSet<string> GetUserRoles()
        {
            HashSet<string> roles = new();
            foreach (var role in User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value))
            {
                roles.Add(role);
            }
            return roles;
        }

        [HttpGet("reservations")]
        public async Task<IEnumerable<Models.UserAPI.Reservation>> GetReservations()
        {
            IdentityUser? user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return new List<Models.UserAPI.Reservation>();
            }

            return _context.Customers
                .Include(c => c.Reservations)
                .FirstOrDefault(c => c.UserId == user.Id)?
                .Reservations.Select(r => new Models.UserAPI.Reservation
                {
                    StartTime = r.StartTime,
                    Guests = r.Guests,
                    Comments = r.Comments ?? "",
                    ReservationId = r.Id
                }) ?? new List<Models.UserAPI.Reservation>();
        }
    }
}
