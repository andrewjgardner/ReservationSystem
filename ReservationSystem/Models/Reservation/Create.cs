﻿using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models.Reservation
{

    public class Create
    {
		public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }

        public int Guests { get; set; }

        public string Comments { get; set; }

        public int SittingId { get; set; }

        public ReservationFormPartial ReservationFormPartial { get; set; }
        
    }
}
