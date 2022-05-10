using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ReservationSystem.Services;

namespace ReservationSystem.Data.Utilities
{
    public class IdentitySeed
    {
        private ApplicationDbContext _context;
        private RoleStore<IdentityRole> _roleStore;
        private PasswordHasher<IdentityUser> _passwordHasher;
        private UserStore<IdentityUser> _userStore;
        private PersonService _personService;

        public IdentitySeed(ApplicationDbContext context, PersonService personService )
        {
            _context = context;
            _roleStore = new RoleStore<IdentityRole>(context);
            _userStore = new UserStore<IdentityUser>(context);
            _passwordHasher = new PasswordHasher<IdentityUser>(); 
            _personService = personService;
        }
        public async Task SeedManager()
        {
            var user = new IdentityUser
            {
                UserName = "manager@manager.com",
                NormalizedUserName = "MANAGER@MANAGER.COM",
                Email = "manager@manager.com",
                NormalizedEmail = "MANAGER@MANAGER.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (!_context.Roles.Any(r => r.Name == "Manager"))
            {
                await _roleStore.CreateAsync(new IdentityRole { Name = "Manager", NormalizedName = "MANAGER" });
            }

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, "manager");
                await _userStore.CreateAsync(user);
                await _userStore.AddToRoleAsync(user, "Manager");
            }

            await _context.SaveChangesAsync();
        }

        public async Task SeedEmployee()
        {
            var user = new IdentityUser
            {
                UserName = "employee@employee.com",
                NormalizedUserName = "EMPLOYEE@EMPLOYEE.COM",
                Email = "employee@employee.com",
                NormalizedEmail = "EMPLOYEE@EMPLOYEE.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (!_context.Roles.Any(r => r.Name == "Employee"))
            {
                await _roleStore.CreateAsync(new IdentityRole { Name = "Employee", NormalizedName = "EMPLOYEE" });
            }

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, "employee");
                await _userStore.CreateAsync(user);
                await _userStore.AddToRoleAsync(user, "Employee");
            }

            await _context.SaveChangesAsync();
        }

        public async Task SeedMember()
        {
            var user = new IdentityUser
            {
                UserName = "member@member.com",
                NormalizedUserName = "MEMBER@MEMBER.COM",
                Email = "member@member.com",
                NormalizedEmail = "MEMBER@MEMBER.COM",
                PhoneNumber = "167761930",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (!_context.Roles.Any(r => r.Name == "Member"))
            {
                await _roleStore.CreateAsync(new IdentityRole { Name = "Member", NormalizedName = "MEMBER" });
            }

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, "member");
                await _userStore.CreateAsync(user);
                await _userStore.AddToRoleAsync(user, "Member");

                var customer = await _personService.FindOrCreateCustomerAsync(1, user.PhoneNumber, "John", "Smith", user.Email);
                customer.UserId = await _userStore.GetUserIdAsync(user);

                await _context.SaveChangesAsync();
            }

        }
    }
}
