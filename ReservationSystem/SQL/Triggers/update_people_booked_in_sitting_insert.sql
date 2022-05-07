CREATE TRIGGER update_people_booked_in_sitting_insert
ON Reservations
AFTER INSERT
AS
BEGIN
	DECLARE @PeopleBooked INT
	SET @PeopleBooked = (SELECT PeopleBooked FROM Sittings WHERE Id = (SELECT SittingId FROM Inserted))
	UPDATE Sittings
	SET Sittings.PeopleBooked = @PeopleBooked + (SELECT Guests FROM Inserted)
	WHERE Id = (SELECT SittingId FROM Inserted)
END