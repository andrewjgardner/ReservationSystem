using Microsoft.EntityFrameworkCore;
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

        public async Task<Person> FindOrCreatePersonAsync(string phoneNumber, string firstName, string lastName)
        {
            var person = await _context.People.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
            if (person == null)
            {
                person = new Person
                {
                    PhoneNumber = phoneNumber,
                    FirstName = firstName,
                    LastName = lastName,
                };
                _context.People.Add(person);
                await _context.SaveChangesAsync();
            }
            return person;
        }

        public async Task<Person> FindOrCreatePersonAsync(string phoneNumber, string firstName, string lastName, string email)
        {
            var person = await _context.People.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
            if (person == null)
            {
                person = new Customer 
                {
                    PhoneNumber = phoneNumber,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email
                };
                _context.People.Add(person);
                await _context.SaveChangesAsync();
            }
            return person;
        }
    }
}
