using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationSystem.Migrations.ApplicationDbMigrations
{
    public partial class UpdateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ReservationTable",
                columns: new[] { "ReservationsId", "TablesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 3, 13 },
                    { 3, 14 },
                    { 3, 15 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ReservationTable",
                keyColumns: new[] { "ReservationsId", "TablesId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ReservationTable",
                keyColumns: new[] { "ReservationsId", "TablesId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ReservationTable",
                keyColumns: new[] { "ReservationsId", "TablesId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ReservationTable",
                keyColumns: new[] { "ReservationsId", "TablesId" },
                keyValues: new object[] { 3, 13 });

            migrationBuilder.DeleteData(
                table: "ReservationTable",
                keyColumns: new[] { "ReservationsId", "TablesId" },
                keyValues: new object[] { 3, 14 });

            migrationBuilder.DeleteData(
                table: "ReservationTable",
                keyColumns: new[] { "ReservationsId", "TablesId" },
                keyValues: new object[] { 3, 15 });
        }
    }
}
