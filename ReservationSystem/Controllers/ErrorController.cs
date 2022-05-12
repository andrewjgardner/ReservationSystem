using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Models;
using System.Diagnostics;

namespace ReservationSystem.Controllers
{
    public class ErrorController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Exception()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ExceptionMessage = TempData["ErrorMessage"]?.ToString() ?? "Error" });
        }

        [Route("/Error/{statusCode}")]
        public IActionResult StatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                    break;
                default:
                    ViewBag.ErrorMessage = "An unknown error has occured";
                    break;
            }
            return View("StatusCodeHandler");
        }

    }
}
