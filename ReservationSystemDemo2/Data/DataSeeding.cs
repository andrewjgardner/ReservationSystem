using Microsoft.EntityFrameworkCore;

namespace ReservationSystem.Data
{
    public class DataSeeding
    {

        ModelBuilder _modelBuilder;

        public DataSeeding(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            SeedRestaurant();
            SeedArea();
            SeedTables();
            SeedSittingType();
            SeedSitting();
            SeedReservation();
            SeedReservationOrigin();
            SeedCustomer();
            SeedEmployee();
            SeedPerson();
            SeedReservationStatus();

        }
        private void SeedRestaurant()
        {
            _modelBuilder.Entity<Restaurant>()
                .HasData(new Restaurant
                {
                    Id = 1,
                    Name = "Bean Scene",
                    Address = "12 Springfield rd",
                    PhoneNumber = "12345678"
                });
        }
        private void SeedArea()
        {
            _modelBuilder.Entity<Area>()
                .HasData(new Area
                {
                    Id = 1,
                    RestaurantId = 1,
                    Name = "Main",
                }, new Area
                {
                    Id = 2,
                    RestaurantId = 1,
                    Name = "Outside"
                }, new Area
                {
                    Id = 3,
                    RestaurantId = 1,
                    Name = "Balcony"
                });
        }

        private void SeedTables()
        {
            _modelBuilder.Entity<Table>()
                .HasData(new Table
                {
                    Id = 1,
                    TableNumber = "M1",
                    TableCapacity = 5,
                    AreaId = 1
                }, new Table
                {
                    Id = 2,
                    TableNumber = "M2",
                    TableCapacity = 3,
                    AreaId = 1
                }, new Table
                {
                    Id = 3,
                    TableNumber = "M3",
                    TableCapacity = 4,
                    AreaId = 1
                }, new Table
                {
                    Id = 4,
                    TableNumber = "M4",
                    TableCapacity = 2,
                    AreaId = 1
                }, new Table
                {
                    Id = 5,
                    TableNumber = "M5",
                    TableCapacity = 5,
                    AreaId = 1
                }, new Table
                {
                    Id = 6,
                    TableNumber = "M6",
                    TableCapacity = 3,
                    AreaId = 1
                }, new Table
                {
                    Id = 7,
                    TableNumber = "M7",
                    TableCapacity = 3,
                    AreaId = 1
                }, new Table
                {
                    Id = 8,
                    TableNumber = "M8",
                    TableCapacity = 3,
                    AreaId = 1
                }, new Table
                {
                    Id = 9,
                    TableNumber = "M9",
                    TableCapacity = 3,
                    AreaId = 1
                }, new Table
                {
                    Id = 10,
                    TableNumber = "M10",
                    TableCapacity = 3,
                    AreaId = 1
                }, new Table
                {
                    Id = 11,
                    TableNumber = "O1",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    Id = 12,
                    TableNumber = "O2",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    Id = 13,
                    TableNumber = "O3",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    Id = 14,
                    TableNumber = "O4",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    Id = 15,
                    TableNumber = "O5",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    Id = 16,
                    TableNumber = "O6",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    Id = 17,
                    TableNumber = "O7",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    Id = 18,
                    TableNumber = "O8",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    Id = 19,
                    TableNumber = "O9",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    Id = 20,
                    TableNumber = "O10",
                    TableCapacity = 3,
                    AreaId = 2
                }, new Table
                {
                    Id = 21,
                    TableNumber = "B1",
                    TableCapacity = 3,
                    AreaId = 3
                }, new Table
                {
                    Id = 22,
                    TableNumber = "B2",
                    TableCapacity = 3,
                    AreaId = 3
                }, new Table
                {
                    Id = 23,
                    TableNumber = "B3",
                    TableCapacity = 3,
                    AreaId = 3
                }, new Table
                {
                    Id = 24,
                    TableNumber = "B4",
                    TableCapacity = 3,
                    AreaId = 3
                }, new Table
                {
                    Id = 25,
                    TableNumber = "B5",
                    TableCapacity = 3,
                    AreaId = 3
                }, new Table
                {
                    Id = 26,
                    TableNumber = "B6",
                    TableCapacity = 3,
                    AreaId = 3
                }, new Table
                {
                    Id = 27,
                    TableNumber = "B7",
                    TableCapacity = 3,
                    AreaId = 3
                }, new Table
                {
                    Id = 28,
                    TableNumber = "B8",
                    TableCapacity = 3,
                    AreaId = 3
                }, new Table
                {
                    Id = 29,
                    TableNumber = "B9",
                    TableCapacity = 3,
                    AreaId = 3
                }, new Table
                {
                    Id = 30,
                    TableNumber = "B10",
                    TableCapacity = 3,
                    AreaId = 3
                });

        }


        private void SeedSittingType()
        {
            _modelBuilder.Entity<SittingType>()
                .HasData(new SittingType
                {
                    Id = 1,
                    Description = "Breakfast"
                }, new SittingType
                {
                    Id = 2,
                    Description = "Lunch"
                }, new SittingType
                {
                    Id = 3,
                    Description = "Dinner"
                });
        }

        private void SeedSitting()
        {
            _modelBuilder.Entity<Sitting>()
                .HasData(new Sitting
                {
                    Id = 1,
                    Title = "Breakfast",
                    StartTime = new DateTime(2020, 04, 13, 7, 0, 0),
                    EndTime = new DateTime(2020, 04, 13, 11, 30, 0),
                    Capacity = 100
                }, new Sitting
                {
                    Id = 2,
                    Title = "Lunch",
                    StartTime = new DateTime(2020, 04, 13, 12, 0, 0),
                    EndTime = new DateTime(2020, 04, 13, 15, 30, 0),
                    Capacity = 100
                }, new Sitting
                {
                    Id = 3,
                    Title = "Dinner",
                    StartTime = new DateTime(2020, 04, 13, 18, 0, 0),
                    EndTime = new DateTime(2020, 04, 13, 21, 30, 0),
                    Capacity = 100
                });
        }

        private void SeedReservation()
        {
            _modelBuilder.Entity<Reservation>()
                .HasData(new Reservation
                {
                    Id = 1,
                    StartTime = new DateTime(2022, 04, 13, 09, 30, 00),
                    NoOfPeople = 3,
                    SittingId = 1,
                    ReservationStatusId = 1,
                    ReservationOriginId = 1,
                    CustomerId = 1,
                }, new Reservation
                {
                    Id = 2,
                    StartTime = new DateTime(2022, 04, 13, 12, 30, 00),
                    NoOfPeople = 4,
                    SittingId = 2,
                    ReservationStatusId = 2,
                    ReservationOriginId = 2,
                    CustomerId = 2,
                }, new Reservation
                {
                    Id = 3,
                    StartTime = new DateTime(2022, 04, 13, 18, 30, 00),
                    NoOfPeople = 5,
                    SittingId = 3,
                    ReservationStatusId = 3,
                    ReservationOriginId = 3,
                    CustomerId = 3,
                });
        }

        private void SeedReservationOrigin()
        {
            _modelBuilder.Entity<ReservationOrigin>()
                .HasData(new ReservationOrigin
                {
                    Id = 1,
                    Description = "Online"
                }, new ReservationOrigin
                {
                    Id = 2,
                    Description = "Phone"
                }, new ReservationOrigin
                {
                    Id = 3,
                    Description = "Walk-in"
                });
        }

        private void SeedReservationStatus()
        {
            _modelBuilder.Entity<ReservationStatus>()
                .HasData(new ReservationStatus
                {
                    Id = 1,
                    Description = "Pending"
                }, new ReservationStatus
                {
                    Id = 2,
                    Description = "Confirmed"
                }, new ReservationStatus
                {
                    Id = 3,
                    Description = "Cancelled"
                });
        }

        private void SeedPerson()
        {
            _modelBuilder.Entity<Person>()
                .HasData(new Person
                {
                    Id = 1,
                    FirstName = "Damien",
                    LastName = "Antonietti",
                    PhoneNumber = "015723892",
                    RestaurantId = 1
                    
                }, new Person
                {
                    Id = 2,
                    FirstName = "Andrew",
                    LastName = "Gardner",
                    PhoneNumber = "015656165",
                    RestaurantId = 1
                }, new Person
                {
                    Id = 3,
                    FirstName = "Brendan",
                    LastName = "Chappell",
                    PhoneNumber = "015723832",
                    RestaurantId = 1
                }, new Person
                {
                    Id = 4,
                    FirstName = "Conor",
                    LastName = "O'Neill",
                    PhoneNumber = "015725832",
                    RestaurantId = 1
                });
        }
        
        private void SeedEmployee()
        {
            _modelBuilder.Entity<Employee>()
                .HasData(new Employee
                {
                    Id = 1,
                }, new Employee
                {
                    Id = 2,
                }, new Employee
                {
                    Id = 3,
                }, new Employee
                {
                    Id = 4,
                });
        }

        private void SeedCustomer()
        {
            _modelBuilder.Entity<Customer>()
                .HasData(new Customer
                {
                    Id = 1,
                    Email = "d.d@d.com"
                }, new Customer
                {
                    Id = 2,
                    Email = "c.c@c.com"
                }, new Customer
                {
                    Id = 3,
                    Email = "b.b@b.com"

                }, new Customer
                {
                    Id = 4,
                    Email = "a.a@a.com"
                });
        }
        


    }
}
