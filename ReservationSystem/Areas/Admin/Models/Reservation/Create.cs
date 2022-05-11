﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ReservationSystem.Areas.Admin.Models.Reservation
{
    public class Create
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public ReservationSystem.Models.Reservation.ReservationForm ReservationForm { get; set; }
        public SelectList? ReservationStatus { get; set; }
        public int ReservationStatusId { get; set; }

        public SelectList? ReservationOrigin { get; set; }
        public int ReservationOriginId { get; set; }

        public SelectList? Sittings { get; set; }
        public int SittingId { get; set; }


    }


}
