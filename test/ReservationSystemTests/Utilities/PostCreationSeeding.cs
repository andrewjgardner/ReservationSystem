﻿using ReservationSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystemTests.Utilities
{
    internal class PostCreationSeeding
    {
        public static void InitializeDbForRead(ApplicationDbContext context)
        {
            context.Restaurants.AddRange(GetRestaurants());
            context.SaveChanges(); 

            context.Areas.AddRange(GetAreas());
            context.SaveChanges(); 

            context.Tables.AddRange(GetTables());
            context.SaveChanges(); 

            context.SittingTypes.AddRange(GetSittingTypes());
            context.SaveChanges(); 

            context.Sittings.AddRange(GetSittings());
            context.SaveChanges(); 

            context.ReservationOrigins.AddRange(GetReservationOrigins());
            context.SaveChanges(); 

            context.ReservationStatuses.AddRange(GetReservationStatuses());
            context.SaveChanges(); 

            context.People.AddRange(GetPeople());
            context.SaveChanges(); 

            context.Customers.AddRange(GetCustomers());
            context.SaveChanges(); 

            context.Employees.AddRange(GetEmployees());
            context.SaveChanges(); 

            context.Reservations.AddRange(GetReservations());
            context.SaveChanges(); 

            context.ReservationTables.AddRange(GetReservationTables());
            context.SaveChanges(); 


        }

        public void InitializeForWrite(ApplicationDbContext context)
        {
            context.Restaurants.AddRange(GetRestaurants());
            context.SaveChanges(); 

            context.Areas.AddRange(GetAreas());
            context.SaveChanges(); 

            context.Tables.AddRange(GetTables());
            context.SaveChanges(); 

            context.SittingTypes.AddRange(GetSittingTypes());
            context.SaveChanges(); 

            context.ReservationOrigins.AddRange(GetReservationOrigins());
            context.SaveChanges(); 

            context.ReservationStatuses.AddRange(GetReservationStatuses());
            context.SaveChanges(); 
        }

        public static List<Restaurant> GetRestaurants()
        {
            return new List<Restaurant>()
            {
                new Restaurant
                {
                    Name = "MEME SCENE",
                    Address = "12 Springfield rd",
                    PhoneNumber = "12345678"
                }
            };
        }

        public static List<Area> GetAreas()
        {
            return new List<Area>()
            {
               new Area
                {
                    RestaurantId = 1,
                    Name = "Main",
                }, new Area
                {
                    RestaurantId = 1,
                    Name = "Outside"
                }, new Area
                {
                    RestaurantId = 1,
                    Name = "Balcony"
                }
           };
        }

        public static List<Table> GetTables()
        {
            return new List<Table>()
            {
                new Table
                {
                    TableNumber = "M1",
                    TableCapacity = 5,
                    AreaId = 1
                }, new Table
                {
                    TableNumber = "M2",
                    TableCapacity = 3,
                    AreaId = 1
                }, new Table
                {
                    TableNumber = "M3",
                    TableCapacity = 4,
                    AreaId = 1
                }, new Table
                {
                    TableNumber = "M4",
                    TableCapacity = 2,
                    AreaId = 1
                }, new Table
                {
                    TableNumber = "M5",
                    TableCapacity = 5,
                    AreaId = 1
                }, new Table
                {
                    TableNumber = "M6",
                    TableCapacity = 3,
                    AreaId = 1
                }, new Table
                {
                    TableNumber = "M7",
                    TableCapacity = 3,
                    AreaId = 1
                }, new Table
                {
                    TableNumber = "M8",
                    TableCapacity = 3,
                    AreaId = 1
                }, new Table
                {
                    TableNumber = "M9",
                    TableCapacity = 3,
                    AreaId = 1
                }, new Table
                {
                    TableNumber = "M10",
                    TableCapacity = 3,
                    AreaId = 1
                }, new Table
                {
                    TableNumber = "O1",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    TableNumber = "O2",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    TableNumber = "O3",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    TableNumber = "O4",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    TableNumber = "O5",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    TableNumber = "O6",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    TableNumber = "O7",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    TableNumber = "O8",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    TableNumber = "O9",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    TableNumber = "O10",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    TableNumber = "B1",
                    TableCapacity = 3,
                    AreaId = 3
                }, new Table
                {
                    TableNumber = "B2",
                    TableCapacity = 3,
                    AreaId = 3
                }, new Table
                {
                    TableNumber = "B3",
                    TableCapacity = 3,
                    AreaId = 3
                }, new Table
                {
                    TableNumber = "B4",
                    TableCapacity = 3,
                    AreaId = 3
                }, new Table
                {
                    TableNumber = "B5",
                    TableCapacity = 3,
                    AreaId = 3
                }, new Table
                {
                    TableNumber = "B6",
                    TableCapacity = 3,
                    AreaId = 3
                }, new Table
                {
                    TableNumber = "B7",
                    TableCapacity = 3,
                    AreaId = 3
                }, new Table
                {
                    TableNumber = "B8",
                    TableCapacity = 3,
                    AreaId = 3
                }, new Table
                {
                    TableNumber = "B9",
                    TableCapacity = 3,
                    AreaId = 3
                }, new Table
                {
                    TableNumber = "B10",
                    TableCapacity = 3,
                    AreaId = 3
                }
            };
        }

        public static List<SittingType> GetSittingTypes()
        {
            return new List<SittingType>()
            {
                new SittingType
                {
                    Description = "Breakfast",
                    ResDuration = 45
                }, new SittingType
                {
                    Description = "Lunch",
                    ResDuration = 60
                }, new SittingType
                {
                    Description = "Dinner",
                    ResDuration = 90
                }
            };
        }

        public static List<Sitting> GetSittings()
        {

            return new List<Sitting>()
            {
                new Sitting
                {
                    Title = "Breakfast",
                    StartTime = new DateTime(2020, 04, 13, 7, 0, 0),
                    EndTime = new DateTime(2020, 04, 13, 11, 30, 0),
                    Capacity = 100,
                    RestaurantId = 1,
                    SittingTypeId = 1,
                    ResDuration = 45
                }, new Sitting
                {
                    Title = "Lunch",
                    StartTime = new DateTime(2020, 04, 13, 12, 0, 0),
                    EndTime = new DateTime(2020, 04, 13, 15, 30, 0),
                    Capacity = 100,
                    RestaurantId = 1,
                    SittingTypeId = 2
                }, new Sitting
                {
                    Title = "Dinner",
                    StartTime = new DateTime(2020, 04, 13, 18, 0, 0),
                    EndTime = new DateTime(2020, 04, 13, 21, 30, 0),
                    Capacity = 100,
                    RestaurantId = 1,
                    SittingTypeId = 3
                }, new Sitting
                {
                    Title = "Dinner",
                    StartTime = new DateTime(2020, 04, 13, 18, 0, 0),
                    EndTime = new DateTime(2020, 04, 13, 21, 30, 0),
                    Capacity = 100,
                    RestaurantId = 1,
                    SittingTypeId = 3
                }
            };
        }

        public static List<Reservation> GetReservations()
        {
            return new List<Reservation>()
           {
               new Reservation
                {
                    StartTime = new DateTime(2022, 04, 13, 09, 30, 00),
                    Guests = 3,
                    SittingId = 1,
                    ReservationStatusId = 1,
                    ReservationOriginId = 1,
                    CustomerId = 1,
                }, new Reservation
                {
                    StartTime = new DateTime(2022, 04, 13, 12, 30, 00),
                    Guests = 4,
                    SittingId = 2,
                    ReservationStatusId = 2,
                    ReservationOriginId = 2,
                    CustomerId = 2,
                }, new Reservation
                {
                    StartTime = new DateTime(2022, 04, 13, 18, 30, 00),
                    Guests = 5,
                    SittingId = 3,
                    ReservationStatusId = 3,
                    ReservationOriginId = 3,
                    CustomerId = 3,
                }
           };
        }

        public static List<ReservationTable> GetReservationTables()
        {
            return new List<ReservationTable>()
           {
                 new ReservationTable
                 {
                     ReservationId = 1,
                     TableId = 3
                 }, new ReservationTable
                 {
                     ReservationId = 1,
                     TableId = 4
                 }, new ReservationTable
                 {
                     ReservationId = 2,
                     TableId = 25
                 }, new ReservationTable
                 {
                     ReservationId = 3,
                     TableId = 13
                 }, new ReservationTable
                 {
                     ReservationId = 3,
                     TableId = 14
                 }, new ReservationTable
                 {
                     ReservationId = 3,
                     TableId = 15
                 }
           };
        }


        public static List<ReservationOrigin> GetReservationOrigins()
        {
            return new List<ReservationOrigin>()
            {
                new ReservationOrigin
                {
                    Description = "Online"
                }, new ReservationOrigin
                {
                    Description = "Phone"
                }, new ReservationOrigin
                {
                    Description = "Walk-in"
                }
            };
        }

        public static List<ReservationStatus> GetReservationStatuses()
        {
            return new List<ReservationStatus>()
            {
                new ReservationStatus
                {
                    Description = "Pending"
                }, new ReservationStatus
                {
                    Description = "Confirmed"
                }, new ReservationStatus
                {
                    Description = "Cancelled"
                }
            };
        }

        public static List<Person> GetPeople()
        {
            return new List<Person>()
            {
                new Person
                {
                    FirstName = "Damien",
                    LastName = "Antonietti",
                    PhoneNumber = "015723892",
                    RestaurantId = 1

                }, new Person
                {
                    FirstName = "Andrew",
                    LastName = "Gardner",
                    PhoneNumber = "015656165",
                    RestaurantId = 1
                }, new Person
                {
                    FirstName = "Brendan",
                    LastName = "Chappell",
                    PhoneNumber = "015723832",
                    RestaurantId = 1
                }, new Person
                {
                    FirstName = "Conor",
                    LastName = "O'Neill",
                    PhoneNumber = "015725832",
                    RestaurantId = 1
                }
            };
        }

        public static List<Employee> GetEmployees()
        {
            return new List<Employee>()
            {
                new Employee
                {
                    EmployeeId = 1,
                    FirstName = "Kaleena",
                    LastName = "Byrne",
                    PhoneNumber = "023457123",
                    RestaurantId = 1
                }, new Employee
                {
                    EmployeeId = 2,
                    FirstName = "Kathleen",
                    LastName = "Smith",
                    PhoneNumber = "0298833412",
                    RestaurantId = 1
                }, new Employee
                {
                    EmployeeId = 3,
                    FirstName = "Jim",
                    LastName = "Jones",
                    PhoneNumber = "023465123",
                    RestaurantId = 1
                }, new Employee
                {
                    EmployeeId = 4,
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "023465153",
                    RestaurantId = 1
                }
            };
        }

        public static List<Customer> GetCustomers()
        {
            return new List<Customer>()
            {
               new Customer
                {
                    FirstName = "Emily",
                    LastName = "Smith",
                    PhoneNumber = "023462343",
                    RestaurantId = 1,
                    Email = "d.d@d.com"
                }, new Customer
                {
                    FirstName = "Frederique",
                    LastName = "Corbyn",
                    PhoneNumber = "0232341789",
                    RestaurantId = 1,
                    Email = "c.c@c.com"
                }, new Customer
                {
                    FirstName = "John",
                    LastName = "Smith",
                    PhoneNumber = "3644253462",
                    RestaurantId = 1,
                    Email = "b.b@b.com"

                }, new Customer
                {
                    FirstName = "William",
                    LastName = "Kemshell",
                    PhoneNumber = "023456789",
                    RestaurantId = 1,
                    Email = "a.a@a.com"
                }
            };
        }
        
    }
}
