﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationSystem.Migrations.ApplicationDbMigrations
{
    public partial class TriggerSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE TRIGGER update_people_booked_in_sitting_insert
                ON Reservations
                AFTER INSERT
                AS
                BEGIN

                    DECLARE @PeopleBooked INT

                    SET @PeopleBooked = (SELECT PeopleBooked FROM Sittings WHERE Id = (SELECT SittingId FROM Inserted))
	                UPDATE Sittings

                    SET Sittings.PeopleBooked = @PeopleBooked + (SELECT Guests FROM Inserted)
	                WHERE Id = (SELECT SittingId FROM Inserted)
                END"
            );

			migrationBuilder.Sql(
				@"CREATE TRIGGER update_people_booked_in_sitting_update
				ON Reservations
				AFTER UPDATE
				AS
				BEGIN
					DECLARE @PeopleBooked INT
					SET @PeopleBooked = (SELECT PeopleBooked FROM Sittings WHERE Id = (SELECT SittingId FROM Deleted))

					DECLARE @ReservationStatus VARCHAR(MAX)
					SET @ReservationStatus = (SELECT Description FROM ReservationStatuses WHERE ( Id = (SELECT Id FROM Inserted )))

					DECLARE @OldSittingId INT
					SET @OldSittingId = (SELECT SittingID FROM Deleted)

					DECLARE @NewSittingId INT
					SET @NewSittingId = (SELECT SittingID FROM Inserted)

					IF @ReservationStatus = 'Cancelled'
					BEGIN
						UPDATE Sittings
						SET Sittings.PeopleBooked = @PeopleBooked - (SELECT Guests FROM Deleted)
						WHERE Id = (SELECT SittingId FROM Deleted)
					END

					IF @OldSittingId != @NewSittingId
					BEGIN
						UPDATE Sittings
						SET Sittings.PeopleBooked = @PeopleBooked - (SELECT Guests FROM Deleted)
						WHERE Id = (SELECT SittingId FROM Deleted)

						UPDATE Sittings
						SET Sittings.PeopleBooked = @PeopleBooked + (SELECT Guests FROM Inserted)
						WHERE Id = (SELECT SittingId FROM Inserted)
					END

					ELSE
					BEGIN
						UPDATE Sittings
						SET Sittings.PeopleBooked = @PeopleBooked - (SELECT Guests FROM Deleted) + (SELECT Guests FROM Inserted)
						WHERE Id = (SELECT SittingId FROM Deleted)
					END

				END"
			);

		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP TRIGGER IF EXISTS update_people_booked_in_sitting_insert, update_people_booked_in_sitting_update");

		}
    }
}
