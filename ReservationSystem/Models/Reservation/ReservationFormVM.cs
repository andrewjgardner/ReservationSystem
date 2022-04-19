﻿using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models.Reservation
{

    public class ReservationFormVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime Time { get; set; }

        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }

        [Required]
        public int NumberOfPeople { get; set; }

        [Required]
        public string Message { get; set; }

        public int SittingId { get; set; }
        
    }
}