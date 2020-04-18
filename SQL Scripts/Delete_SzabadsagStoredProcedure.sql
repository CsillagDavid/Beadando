USE Nyilvantartas
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'DeleteSzabadsagok' 
)
   DROP PROCEDURE [dbo].[DeleteSzabadsagok]
GO

CREATE PROCEDURE [dbo].[DeleteSzabadsagok]
	@Datum DATE,
	@FelhasznaloID INT
AS
	BEGIN TRAN
		IF EXISTS (SELECT * FROM [dbo].[Szabadsagok] 
			WHERE Datum = @Datum AND FelhasznaloID=@FelhasznaloID)
		BEGIN
			DELETE FROM [dbo].[Szabadsagok]
			WHERE Datum = @Datum  AND FelhasznaloID=@FelhasznaloID
		END
	COMMIT TRAN
RETURN 0
GO