﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace ReservationSystem.Areas.Admin.Models.User
{
    public class Create
    {
        public string RoleName { get; set; }
        public SelectList Roles { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
