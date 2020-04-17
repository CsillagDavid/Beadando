USE Nyilvantartas
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'DeleteUnnepnapok' 
)
   DROP PROCEDURE [dbo].[DeleteUnnepnapok]
GO

CREATE PROCEDURE [dbo].[DeleteUnnepnapok]
	@Datum DATE
AS
	BEGIN TRAN
		IF EXISTS (SELECT * FROM [dbo].[Unnepnapok] 
			WHERE Datum = @Datum)
		BEGIN
			DELETE FROM [dbo].[Unnepnapok]
			WHERE Datum = @Datum
		END
	COMMIT TRAN
RETURN 0
GO