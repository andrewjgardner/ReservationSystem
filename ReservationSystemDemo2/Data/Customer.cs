﻿namespace ReservationSystem.Data
{
    public class Customer : Person
    {
        public int Id { get; set; }
        public string Email { get; set; }


        public List<Reservation> Reservations = new List<Reservation>();
    }
}
