﻿using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Data.Context;

namespace ReservationSystem.Services
{
    public class PersonService
    {
        private readonly ApplicationDbContext _context;

        public PersonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Person> FindOrCreatePersonAsync(int restaurantId, string phoneNumber, string firstName, string lastName, string email)
        {
            var person = await _context.People.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
            if (person == null)
            {
                person = new Person
                {
                    PhoneNumber = phoneNumber,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    RestaurantId = restaurantId
                };
                _context.People.Add(person);
                await _context.SaveChangesAsync();
            }
            return person;
        }

        public async Task<Customer> FindOrCreateCustomerAsync(int restaurantId, string firstName, string lastName, string email, string? phoneNumber=null)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(p => p.Email == email);
            if (customer == null )
            {
                customer = new Customer 
                {
                    PhoneNumber = phoneNumber,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    RestaurantId = restaurantId
                };
                _context.People.Add(customer);
                await _context.SaveChangesAsync();
            }
            return customer;
        }
    }
}
