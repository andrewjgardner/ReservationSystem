using ReservationSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystemTests.Utilities
{
    internal class PostCreationSeeding
    {
        public static void InitializeDbForSittings(ApplicationDbContext context)
        {
            context.Sittings.AddRange(GetSittings());
            context.SaveChanges();
        }

        public static List<Sitting> GetSittings()
        {

            return new List<Sitting>()
            {
                new Sitting
                {
                    Id = 1,
                    Title = "Breakfast",
                    StartTime = new DateTime(2020, 04, 13, 7, 0, 0),
                    EndTime = new DateTime(2020, 04, 13, 11, 30, 0),
                    Capacity = 100,
                    RestaurantId = 1,
                    SittingTypeId = 1,
                    ResDuration = 45
                }, new Sitting
                {
                    Id = 2,
                    Title = "Lunch",
                    StartTime = new DateTime(2020, 04, 13, 12, 0, 0),
                    EndTime = new DateTime(2020, 04, 13, 15, 30, 0),
                    Capacity = 100,
                    RestaurantId = 1,
                    SittingTypeId = 2
                }, new Sitting
                {
                    Id = 3,
                    Title = "Dinner",
                    StartTime = new DateTime(2020, 04, 13, 18, 0, 0),
                    EndTime = new DateTime(2020, 04, 13, 21, 30, 0),
                    Capacity = 100,
                    RestaurantId = 1,
                    SittingTypeId = 3
                }, new Sitting
                {
                    Id = 4,
                    Title = "Dinner",
                    StartTime = new DateTime(2020, 04, 13, 18, 0, 0),
                    EndTime = new DateTime(2020, 04, 13, 21, 30, 0),
                    Capacity = 100,
                    RestaurantId = 1,
                    SittingTypeId = 3
                }
            };
        }


    }
}
