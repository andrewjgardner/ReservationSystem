using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Data;
using ReservationSystem.Models.SittingsAPI;

namespace ReservationSystem.Controllers
{
    [Route("api/sittings")]
    [ApiController]
    
    public class SittingsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SittingsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{date}")]
        public ICollection<Get> Get(DateTime date)
        {
            var data = _context.Sittings
                .Where(m => m.StartTime.Year == date.Year && m.StartTime.Month == date.Month)
                .Select(s => new Get { Id = s.Id,Title = s.Title, StartTime = s.StartTime, EndTime = s.EndTime})
                .ToList();

            return data;
        }
    }
}
