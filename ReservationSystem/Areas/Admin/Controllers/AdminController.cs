using Microsoft.AspNetCore.Mvc;

namespace ReservationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        public AdminController ()
        {

        }

        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
