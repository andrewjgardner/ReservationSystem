CREATE PROCEDURE check_triggers @name NVARCHAR(MAX)
AS

SELECT
    *
FROM 
    sys.triggers
WHERE
    name = @name