using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationSystem.Migrations.ApplicationDbMigrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQL", "Seed", "seed_data.sql");
            migrationBuilder.Sql(File.ReadAllText(file));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
