USE Nyilvantartas
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'InsertOrUpdateMunkaidok' 
)
   DROP PROCEDURE [dbo].[InsertOrUpdateMunkaidok]
GO

CREATE PROCEDURE [dbo].[InsertOrUpdateMunkaidok]
	@Datum DATE, 
	@Kezdo_Ido DECIMAL(18,0), 
	@Befejezo_Ido DECIMAL(18,0), 
	@FelhasznaloID INT
AS
	BEGIN TRAN
		IF EXISTS (SELECT * FROM [dbo].[Munkaidok] 
			WHERE Datum = @Datum AND FelhasznaloID = @FelhasznaloID)
		BEGIN
		   UPDATE [dbo].[Munkaidok] 
		   SET 
				Datum = @Datum, 
				Kezdo_ido = @Kezdo_Ido, 
				Befejezo_ido = @Befejezo_Ido
		   WHERE Datum = @Datum AND FelhasznaloID = @FelhasznaloID
		END
		ELSE
		BEGIN
		   INSERT INTO [dbo].[Munkaidok] (Datum, Kezdo_ido, Befejezo_ido, FelhasznaloID) 
		   VALUES (@Datum, @Kezdo_Ido, @Befejezo_Ido, @FelhasznaloID)
		END
	COMMIT TRAN
RETURN 0
GO