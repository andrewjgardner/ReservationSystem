using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Data;
using ReservationSystem.Data.Context;
using ReservationSystem.Models.SittingsAPI;

namespace ReservationSystem.Controllers
{
    [Route("api/sitting")]
    [ApiController]
    
    public class SittingAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SittingAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{date}")]
        public ICollection<Get> Get(DateTime date)
        {
            var d = date.ToLocalTime();
            return _context.Sittings
                .Where(m => m.StartTime.Year == d.Year && m.StartTime.Month == d.Month && !m.IsClosed && m.StartTime > DateTime.Now)
                .Select(s => new Get { Id = s.Id, Title = s.Title, StartTime = s.StartTime, EndTime = s.EndTime, resDuration = s.ResDuration })
                .ToList();
        }
    }
}
