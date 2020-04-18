USE Nyilvantartas
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'InsertOrUpdateSzabadsagok' 
)
   DROP PROCEDURE [dbo].[InsertOrUpdateSzabadsagok]
GO

CREATE PROCEDURE [dbo].[InsertOrUpdateSzabadsagok]
	@Datum DATE,
	@Tavollet NVARCHAR(255),
	@FelhasznaloID INT
AS
	BEGIN TRAN
		IF EXISTS (SELECT * FROM [dbo].[Szabadsagok] 
			WHERE Datum = @Datum AND FelhasznaloID = @FelhasznaloID)
		BEGIN
		   UPDATE [dbo].[Szabadsagok] 
		   SET 
				Datum = @Datum,
				Tavollet = @Tavollet
		   WHERE Datum = @Datum AND FelhasznaloID = @FelhasznaloID
		END
		ELSE
		BEGIN
			DELETE FROM [dbo].[Munkaidok]
			WHERE Datum = @Datum AND FelhasznaloID = @FelhasznaloID
		END
		BEGIN
		   INSERT INTO [dbo].[Szabadsagok] (Datum, Tavollet, FelhasznaloID) 
		   VALUES (@Datum, @Tavollet, @FelhasznaloID)
		END
	COMMIT TRAN
RETURN 0
GO