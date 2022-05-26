using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ReservationSystem.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenAPIController : Controller
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IActionResult Index()
        {
            return View();
        }
    }
}
