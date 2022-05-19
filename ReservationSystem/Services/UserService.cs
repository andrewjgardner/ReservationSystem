﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data.Context;

namespace ReservationSystem.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<string> GetUserRoles(string userid)
        {
            var roles = _context.UserRoles
                .Where(ur => ur.UserId == userid)
                .Join(_context.Roles,
                    ur => ur.RoleId,
                    r => r.Id,
                    (ur, r) => r.Name
                )
                .ToList();
            return roles;
        }
    }
}
