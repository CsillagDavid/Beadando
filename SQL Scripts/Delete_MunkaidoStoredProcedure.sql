USE Nyilvantartas
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'DeleteMunkaidok' 
)
   DROP PROCEDURE [dbo].[DeleteMunkaidok]
GO

CREATE PROCEDURE [dbo].[DeleteMunkaidok]
	@Datum DATE,
	@FelhasznaloID INTEGER
AS
	BEGIN TRAN
		IF EXISTS (SELECT * FROM [dbo].[Munkaidok] 
			WHERE Datum = @Datum AND FelhasznaloID=@FelhasznaloID)
		BEGIN
			DELETE FROM [dbo].[Munkaidok]
			WHERE Datum = @Datum  AND FelhasznaloID=@FelhasznaloID
		END
	COMMIT TRAN
RETURN 0
GO