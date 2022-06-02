using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationSystem.Migrations.ApplicationDbMigrations
{
    public partial class AddExtraCustomersToDataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4", 0, "string4", "mobile@man.com", true, false, null, "MOBILE@MAN.COM", "MOBILE@MAN.COM", "AQAAAAEAACcQAAAAEL2BSRUHQM9fCh0jb+mYgX4H+MYYLnYhFdx/1ePZn6q00SRtRboczPPvkyo2ysL3FA==", "167761930", false, "656ddfb3-19a6-4b93-85a4-a60418b96559", false, "mobile@man.com" });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 12,
                column: "UserId",
                value: null);

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Discriminator", "Email", "FirstName", "LastName", "PhoneNumber", "RestaurantId", "UserId" },
                values: new object[,]
                {
                    { 13, "Customer", "mobile@man.com", "Mobile", "Man", "777747774777", 1, "4" },
                    { 14, "Customer", "member@member.com", "Default", "Member", "42513513245", 1, "3" }
                });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "CustomerId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "CustomerId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3,
                column: "CustomerId",
                value: 13);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 12,
                column: "UserId",
                value: "1");

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "CustomerId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "CustomerId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3,
                column: "CustomerId",
                value: 6);
        }
    }
}
