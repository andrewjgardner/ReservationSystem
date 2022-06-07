using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationSystem.Migrations.ApplicationDbMigrations
{
    public partial class AddViewAndProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE VIEW PeopleReservationSittingView AS    
	                SELECT dbo.People.FirstName, dbo.People.LastName, dbo.People.PhoneNumber, dbo.People.Email, dbo.Reservations.Guests, dbo.Reservations.Comments, dbo.Sittings.Title, dbo.Sittings.StartTime AS Expr1, dbo.Sittings.EndTime, dbo.Sittings.Id AS Expr2, dbo.People.Id AS Expr3, dbo.Reservations.StartTime
	                FROM dbo.People
	                INNER JOIN dbo.Reservations ON dbo.People.Id = dbo.Reservations.CustomerId
                	INNER JOIN dbo.Sittings ON dbo.Reservations.SittingId = dbo.Sittings.Id"
                );

            migrationBuilder.Sql(
                @"CREATE PROCEDURE SelectReservations @CustomerID INT, @ReservationStatus INT
                    AS
                    SELECT *
                    FROM Reservations
                WHERE @CustomerID = CustomerId AND @ReservationStatus = ReservationStatusId
                GO"
                );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW PeopleReservationSittingView");

            migrationBuilder.Sql("DROP PROCEDURE SelectReservations");
        }
    }
}
