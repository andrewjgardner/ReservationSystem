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
        public async Task SeedAdmin()
        {
            var user = new IdentityUser
            {
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (!_context.Roles.Any(r => r.Name == "Admin"))
            {
                await _roleStore.CreateAsync(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
            }

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, "admin");
                await _userStore.CreateAsync(user);
                await _userStore.AddToRoleAsync(user, "Admin");
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
