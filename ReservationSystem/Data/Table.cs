﻿namespace ReservationSystem.Data
{
    public class Table
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public int TableCapacity { get; set; }


        public Area Area { get; set; }
        public int AreaId { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}
