﻿namespace ReservationSystem.Data
{
    public class Sitting
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Capacity { get; set; }
        public bool IsClosed { get; set; } = false;
        public int ResDuration { get; set; }
        public int PeopleBooked { get; set; }

        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }

        public SittingType SittingType { get; set; }
        public int SittingTypeId { get; set; }


        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

        public int PercentFull()
        {
            int peoplebooked = 0;
            foreach(Reservation reservation in this.Reservations)
            {
                peoplebooked += reservation.Guests;
            }
            return 100 * peoplebooked / Capacity;
        }
    }
}
