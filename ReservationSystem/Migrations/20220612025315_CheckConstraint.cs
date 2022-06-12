using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationSystem.Migrations.ApplicationDbMigrations
{
    public partial class CheckConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Guests_GreaterThanZero",
                table: "Reservations",
                sql: "[Guests] > 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Guests_GreaterThanZero",
                table: "Reservations");
        }
    }
}
