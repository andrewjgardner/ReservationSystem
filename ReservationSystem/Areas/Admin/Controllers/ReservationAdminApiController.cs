using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data.Context;
using ReservationSystem.Services;

namespace ReservationSystem.Areas.Admin.Controllers
{
    [Route("api/admin/reservations")]
    [ApiController]
    public class ReservationAdminApiController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly PersonService _personService;
  

        public ReservationAdminApiController(ApplicationDbContext context, PersonService personService)
        {
            _personService = personService;
            _context = context;

        }
        //api/admin/reservations/assign-table/1/3
        [Route("assign-table/{reservationId}/{tableId}")]
        public async Task<IActionResult> AssignReservationToTable(int reservationId,int tableId)
        {
            var reservation = await _context.Reservations.Include(r => r.Tables).Include(r => r.Customer).FirstOrDefaultAsync(r => r.Id == reservationId);
            var table = await _context.Tables.FirstOrDefaultAsync(t => t.Id == tableId); 
            if(reservation == null || table == null)
            {
                return BadRequest(); 
            }

            var tableAlreadyAssigned = reservation.Tables.Any(t => t.Id == tableId);
            if (!tableAlreadyAssigned)
            {
                reservation.Tables.Add(table);
                await _context.SaveChangesAsync(); 
            }
            return Ok(new { tableName = table.TableName, reservationCustomerName = reservation.Customer.FullName(), reservationTime = reservation.StartTime.ToShortTimeString(), tableAlreadyAssigned });
        }
    }
}
