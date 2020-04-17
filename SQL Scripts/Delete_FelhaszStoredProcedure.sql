USE Nyilvantartas
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'DeleteFelhasznalok' 
)
   DROP PROCEDURE [dbo].[DeleteFelhasznalok]
GO

CREATE PROCEDURE [dbo].[DeleteFelhasznalok]
	@id INT,
	@Nev NVARCHAR(255),
	@Email NVARCHAR(255)
AS
	BEGIN TRAN
		IF EXISTS (SELECT * FROM [dbo].[Felhasznalok] 
			WHERE Email = @Email and id = @id)
		BEGIN
		IF EXISTS (SELECT * FROM [dbo].[Jogkorok]
			WHERE FelhasznaloID = @id)
			BEGIN
				DELETE FROM [dbo].[Jogkorok]
				WHERE FelhasznaloID = @id
			END
			IF EXISTS (SELECT * FROM [dbo].[Munkaidok]
			WHERE FelhasznaloID = @id)
			BEGIN
				DELETE FROM [dbo].[Munkaidok]
				WHERE FelhasznaloID = @id
			END
			DELETE FROM [dbo].[Felhasznalok]
				WHERE Email = @Email AND Nev = @Nev AND id = @id
		END	
	COMMIT TRAN
RETURN 0
GO