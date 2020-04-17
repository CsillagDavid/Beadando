USE Nyilvantartas
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'InsertOrUpdateUnnepnapok' 
)
   DROP PROCEDURE [dbo].[InsertOrUpdateUnnepnapok]
GO

CREATE PROCEDURE [dbo].[InsertOrUpdateUnnepnapok]
	@Datum DATE, 
	@Tipus INT
AS
	BEGIN TRAN
		IF EXISTS (SELECT * FROM [dbo].[Unnepnapok] 
			WHERE Datum = @Datum)
		BEGIN
		   UPDATE [dbo].[Unnepnapok] 
		   SET 
				Datum = @Datum, 
				Tipus = @Tipus
		   WHERE Datum = @Datum
		END
		ELSE
		BEGIN
		   INSERT INTO [dbo].[Unnepnapok] (Datum, Tipus) 
		   VALUES (@Datum, @Tipus)
		END
	COMMIT TRAN
RETURN 0
GO