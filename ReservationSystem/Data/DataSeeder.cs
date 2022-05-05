﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ReservationSystem.Data
{
    public class DataSeeder
    {

        ModelBuilder _modelBuilder;

        public DataSeeder(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            List<Table> tables = GetTables();
            SeedRestaurant();
            SeedArea();
            SeedTables(tables);
            SeedSittingType();
            SeedSitting();
            SeedReservation(tables);
            //SeedReservationTable();
            SeedReservationOrigin();
            SeedCustomer();
            SeedEmployee();
            SeedPerson();
            SeedReservationStatus();
            SeedRoles();

        }

        public List<Table> GetTables()
        {
            var tables = new List<Table> { new Table
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
                }};
            return tables;
            
        }

        public void SeedRestaurant()
        {
            _modelBuilder.Entity<Restaurant>()
                .HasData(new Restaurant
                {
                    Id = 1,
                    Name = "Bean Scene",
                    Address = "12 Springfield rd",
                    PhoneNumber = "12345678",
                    DefaultCapacity = 100
                });
        }
        public void SeedArea()
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

        public void SeedTables(List<Table> tables)
        {
            _modelBuilder.Entity<Table>().HasData(tables);

        }


        public void SeedSittingType()
        {
            _modelBuilder.Entity<SittingType>()
                .HasData(new SittingType
                {
                    Id = 1,
                    Description = "Breakfast",
                    ResDuration = 45
                }, new SittingType
                {
                    Id = 2,
                    Description = "Lunch",
                    ResDuration = 60
                }, new SittingType
                {
                    Id = 3,
                    Description = "Dinner",
                    ResDuration = 90
                });
        }

        public void SeedSitting()
        {
            _modelBuilder.Entity<Sitting>()
                .HasData(new Sitting
                {
                    Id = 1,
                    Title = "Breakfast",
                    StartTime = new DateTime(2022, 07, 13, 7, 0, 0),
                    EndTime = new DateTime(2022, 07, 13, 11, 30, 0),
                    Capacity = 100,
                    RestaurantId = 1,
                    SittingTypeId = 1,
                    ResDuration = 45
                }, new Sitting
                {
                    Id = 2,
                    Title = "Lunch",
                    StartTime = new DateTime(2022, 07, 13, 12, 0, 0),
                    EndTime = new DateTime(2022, 07, 13, 15, 30, 0),
                    Capacity = 100,
                    RestaurantId = 1,
                    SittingTypeId = 2
                }, new Sitting
                {
                    Id = 3,
                    Title = "Dinner",
                    StartTime = new DateTime(2022, 07, 13, 18, 0, 0),
                    EndTime = new DateTime(2022, 07, 13, 21, 30, 0),
                    Capacity = 100,
                    RestaurantId = 1,
                    SittingTypeId = 3
                });
        }

        public void SeedReservation(List<Table> tables)
        {
            var res1 = new Reservation
            {
                Id = 1,
                StartTime = new DateTime(2022, 07, 13, 09, 30, 00),
                Comments = "By the balcony, please.",
                Guests = 3,
                SittingId = 1,
                ReservationStatusId = 1,
                ReservationOriginId = 1,
                CustomerId = 5,
                Tables = new List<Table> { tables[2], tables[3] }
            };
            var res2 = new Reservation
            {
                Id = 2,
                StartTime = new DateTime(2022, 07, 13, 12, 30, 00),
                Comments = "",
                Guests = 4,
                SittingId = 2,
                ReservationStatusId = 2,
                ReservationOriginId = 2,
                CustomerId = 8,
                Tables = new List<Table> { tables[24] }
            };
            var res3 = new Reservation
            {
                Id = 3,
                StartTime = new DateTime(2022, 07, 13, 18, 30, 00),
                Comments = "",
                Guests = 5,
                SittingId = 3,
                ReservationStatusId = 3,
                ReservationOriginId = 3,
                CustomerId = 6,
                Tables = new List<Table> { tables[12],tables[13],tables[14] }
            };
            _modelBuilder.Entity<Reservation>().HasData(res1,res2,res3);
        }

        /*
        public void SeedReservationTable()
        {
            _modelBuilder.Entity<ReservationTable>()
                 .HasData(new ReservationTable
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
                 });
        }
        */

        public void SeedReservationOrigin()
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

        public void SeedReservationStatus()
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

        public void SeedPerson()
        {
            _modelBuilder.Entity<Person>()
                .HasData(new Person
                {
                    Id = 1,
                    FirstName = "Damien",
                    LastName = "Antonietti",
                    PhoneNumber = "015723892",
                    Email = "g@g.com",
                    RestaurantId = 1

                }, new Person
                {
                    Id = 4,
                    FirstName = "Andrew",
                    LastName = "Gardner",
                    PhoneNumber = "015656165",
                    Email = "h@h.com",
                    RestaurantId = 1
                }, new Person
                {
                    Id = 7,
                    FirstName = "Brendan",
                    LastName = "Chappell",
                    PhoneNumber = "015723832",
                    Email = "j@j.com",
                    RestaurantId = 1
                }, new Person
                {
                    Id = 9,
                    FirstName = "Conor",
                    LastName = "O'Neill",
                    PhoneNumber = "015725832",
                    Email = "k@k.com",
                    RestaurantId = 1
                });
        }

        public void SeedEmployee()
        {
            _modelBuilder.Entity<Employee>()
                .HasData(new Employee
                {
                    Id = 2,
                    EmployeeId = 1,
                    FirstName = "Kaleena",
                    LastName = "Byrne",
                    PhoneNumber = "023457123",
                    Email = "t@k.com",
                    RestaurantId = 1
                }, new Employee
                {
                    Id = 3,
                    EmployeeId = 2,
                    FirstName = "Kathleen",
                    LastName = "Smith",
                    PhoneNumber = "0298833412",
                    Email = "ok@k.com",
                    RestaurantId = 1
                }, new Employee
                {
                    Id = 10,
                    EmployeeId = 3,
                    FirstName = "Jim",
                    LastName = "Jones",
                    PhoneNumber = "023465123",
                    Email = "pat@k.com",
                    RestaurantId = 1
                }, new Employee
                {
                    Id = 11,
                    EmployeeId = 4,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@k.com",
                    PhoneNumber = "023465153",
                    RestaurantId = 1
                });
        }

        public void SeedCustomer()
        {
            _modelBuilder.Entity<Customer>()
                .HasData(new Customer
                {
                    Id = 5,
                    FirstName = "Emily",
                    LastName = "Smith",
                    PhoneNumber = "023462343",
                    RestaurantId = 1,
                    Email = "emilyd@d.com"
                }, new Customer
                {
                    Id = 6,
                    FirstName = "Frederique",
                    LastName = "Corbyn",
                    PhoneNumber = "0232341789",
                    RestaurantId = 1,
                    Email = "c.c@c.com"
                }, new Customer
                {
                    Id = 8,
                    FirstName = "John",
                    LastName = "Smith",
                    PhoneNumber = "3644253462",
                    RestaurantId = 1,
                    Email = "b.b@b.com"

                }, new Customer
                {
                    Id = 12,
                    FirstName = "William",
                    LastName = "Kemshell",
                    PhoneNumber = "023456789",
                    RestaurantId = 1,
                    Email = "a.a@a.com"
                });
        }

        private void SeedRoles()
        {
            _modelBuilder.Entity<IdentityRole>()
                .HasData(new IdentityRole
                {
                    Id = "1",
                    Name = Roles.Admin.ToString(),
                    NormalizedName = Roles.Admin.ToString().ToUpper()
                }, new IdentityRole
                {
                    Id = "2",
                    Name = Roles.Employee.ToString(),
                    NormalizedName = Roles.Employee.ToString().ToUpper()
                }, new IdentityRole
                {
                    Id = "3",
                    Name = Roles.Member.ToString(),
                    NormalizedName = Roles.Member.ToString().ToUpper()
                });
        }


    }
}
