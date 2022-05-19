using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationSystem.Migrations.ApplicationDbMigrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "00000000-0000-0000-0000-000000000000", "Manager", "MANAGER" },
                    { "2", "00000000-0000-0000-0000-000000000000", "Employee", "EMPLOYEE" },
                    { "3", "00000000-0000-0000-0000-000000000000", "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "string1", "manager@manager.com", true, false, null, "MANAGER@MANAGER.COM", "MANAGER@MANAGER.COM", "AQAAAAEAACcQAAAAEEjaR1E/iGB+XaDSNNdZM9zczIFeTGpmlEUnvqlvTy29qthma6iq/xtToIGSjS5bYw==", null, false, "656ddfb3-19a6-4b93-85a4-a60418b96559", false, "manager@manager.com" },
                    { "2", 0, "string2", "employee@employee.com", true, false, null, "EMPLOYEE@EMPLOYEE.COM", "EMPLOYEE@EMPLOYEE.COM", "AQAAAAEAACcQAAAAEIh8sUdEg/CJLKzRbuB7qA1p3LRc02ZAjOMtc2pttFiFbxISUy/u0N9gPS3xfmabRw==", null, false, "656ddfb3-19a6-4b93-85a4-a60418b96559", false, "employee@employee.com" },
                    { "3", 0, "string3", "member@member.com", true, false, null, "MEMBER@MEMBER.COM", "MEMBER@MEMBER.COM", "AQAAAAEAACcQAAAAEL2BSRUHQM9fCh0jb+mYgX4H+MYYLnYhFdx/1ePZn6q00SRtRboczPPvkyo2ysL3FA==", "167761930", false, "656ddfb3-19a6-4b93-85a4-a60418b96559", false, "member@member.com" }
                });

            migrationBuilder.InsertData(
                table: "ReservationOrigins",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Online" },
                    { 2, "Phone" },
                    { 3, "Walk-in" }
                });

            migrationBuilder.InsertData(
                table: "ReservationStatuses",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Confirmed" },
                    { 3, "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "DefaultCapacity", "Name", "PhoneNumber" },
                values: new object[] { 1, "12 Springfield rd", 100, "Bean Scene", "12345678" });

            migrationBuilder.InsertData(
                table: "SittingTypes",
                columns: new[] { "Id", "Description", "ResDuration" },
                values: new object[,]
                {
                    { 1, "Breakfast", 45 },
                    { 2, "Lunch", 60 },
                    { 3, "Dinner", 90 }
                });

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "Id", "Name", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "Main", 1 },
                    { 2, "Outside", 1 },
                    { 3, "Balcony", 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "2" },
                    { "3", "3" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Discriminator", "Email", "FirstName", "LastName", "PhoneNumber", "RestaurantId", "UserId" },
                values: new object[,]
                {
                    { 5, "Customer", "emilyd@d.com", "Emily", "Smith", "023462343", 1, null },
                    { 6, "Customer", "c.c@c.com", "Frederique", "Corbyn", "0232341789", 1, null },
                    { 8, "Customer", "b.b@b.com", "John", "Smith", "3644253462", 1, null },
                    { 12, "Customer", "a.a@a.com", "William", "Kemshell", "023456789", 1, "1" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Discriminator", "Email", "EmployeeId", "FirstName", "LastName", "PhoneNumber", "RestaurantId", "UserId" },
                values: new object[,]
                {
                    { 2, "Employee", "t@k.com", 1, "Kaleena", "Byrne", "023457123", 1, null },
                    { 3, "Employee", "ok@k.com", 2, "Kathleen", "Smith", "0298833412", 1, null },
                    { 10, "Employee", "pat@k.com", 3, "Jim", "Jones", "023465123", 1, null },
                    { 11, "Employee", "johndoe@k.com", 4, "John", "Doe", "023465153", 1, null }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Discriminator", "Email", "FirstName", "LastName", "PhoneNumber", "RestaurantId", "UserId" },
                values: new object[,]
                {
                    { 1, "Person", "g@g.com", "Damien", "Antonietti", "015723892", 1, null },
                    { 4, "Person", "h@h.com", "Andrew", "Gardner", "015656165", 1, null },
                    { 7, "Person", "j@j.com", "Brendan", "Chappell", "015723832", 1, null },
                    { 9, "Person", "k@k.com", "Conor", "O'Neill", "015725832", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Sittings",
                columns: new[] { "Id", "Capacity", "EndTime", "IsClosed", "PeopleBooked", "ResDuration", "RestaurantId", "SittingTypeId", "StartTime", "Title" },
                values: new object[,]
                {
                    { 1, 100, new DateTime(2022, 7, 13, 11, 30, 0, 0, DateTimeKind.Unspecified), false, 3, 45, 1, 1, new DateTime(2022, 7, 13, 7, 0, 0, 0, DateTimeKind.Unspecified), "Breakfast" },
                    { 2, 100, new DateTime(2022, 7, 13, 15, 30, 0, 0, DateTimeKind.Unspecified), false, 4, 0, 1, 2, new DateTime(2022, 7, 13, 12, 0, 0, 0, DateTimeKind.Unspecified), "Lunch" },
                    { 3, 100, new DateTime(2022, 7, 13, 21, 30, 0, 0, DateTimeKind.Unspecified), false, 5, 0, 1, 3, new DateTime(2022, 7, 13, 18, 0, 0, 0, DateTimeKind.Unspecified), "Dinner" }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "Comments", "CustomerId", "Guests", "ReservationOriginId", "ReservationStatusId", "SittingId", "StartTime" },
                values: new object[,]
                {
                    { 1, "By the balcony, please.", 5, 3, 1, 1, 1, new DateTime(2022, 7, 13, 9, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "", 8, 4, 2, 2, 2, new DateTime(2022, 7, 13, 12, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "", 6, 5, 3, 3, 3, new DateTime(2022, 7, 13, 18, 30, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "AreaId", "TableCapacity", "TableName" },
                values: new object[,]
                {
                    { 1, 1, 5, "M1" },
                    { 2, 1, 3, "M2" },
                    { 3, 1, 4, "M3" },
                    { 4, 1, 2, "M4" },
                    { 5, 1, 5, "M5" },
                    { 6, 1, 3, "M6" },
                    { 7, 1, 3, "M7" },
                    { 8, 1, 3, "M8" },
                    { 9, 1, 3, "M9" },
                    { 10, 1, 3, "M10" },
                    { 11, 2, 3, "O1" },
                    { 12, 2, 3, "O2" },
                    { 13, 2, 3, "O3" },
                    { 14, 2, 3, "O4" },
                    { 15, 2, 3, "O5" },
                    { 16, 2, 3, "O6" },
                    { 17, 2, 3, "O7" },
                    { 18, 2, 3, "O8" },
                    { 19, 2, 3, "O9" },
                    { 20, 2, 3, "O10" },
                    { 21, 3, 3, "B1" },
                    { 22, 3, 3, "B2" },
                    { 23, 3, 3, "B3" },
                    { 24, 3, 3, "B4" },
                    { 25, 3, 3, "B5" },
                    { 26, 3, 3, "B6" },
                    { 27, 3, 3, "B7" },
                    { 28, 3, 3, "B8" },
                    { 29, 3, 3, "B9" },
                    { 30, 3, 3, "B10" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3", "3" });

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ReservationOrigins",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ReservationOrigins",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ReservationOrigins",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ReservationStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ReservationStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ReservationStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sittings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sittings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sittings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SittingTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SittingTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SittingTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
