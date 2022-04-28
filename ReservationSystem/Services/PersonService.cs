using ReservationSystem.Data;

namespace ReservationSystem.Services
{
    public class PersonService
    {
        private readonly ApplicationDbContext _context;

        public PersonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Person> FindOrCreatePersonAsync(string email, string phonenumber, string firstname, string lastname)
        {
            return;
        }
        
    }
}
