USE Nyilvantartas
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'InsertOrUpdateFelhasznalok' 
)
   DROP PROCEDURE [dbo].[InsertOrUpdateFelhasznalok]
GO

CREATE PROCEDURE [dbo].[InsertOrUpdateFelhasznalok]
	@Nev NVARCHAR(255), 
	@Jelszo NVARCHAR(255), 
	@Email NVARCHAR(255), 
	@Munkaido INT
AS
	BEGIN TRAN
		IF EXISTS (SELECT * FROM [dbo].[Felhasznalok] 
			WHERE Nev = @Nev OR Email = @Email)
		BEGIN
		   UPDATE [dbo].[Felhasznalok] 
		   SET 
				Nev = @Nev, 
				Email = @Email, 
				Jelszo = @Jelszo,
				Munkaido = @Munkaido
		   WHERE Nev = @Nev OR Email = @Email
		END
		ELSE
		BEGIN
		   INSERT INTO [dbo].[Felhasznalok](Nev, Jelszo, Email, Munkaido) 
		   VALUES (@Nev, @Jelszo, @Email, @Munkaido)
		END
	COMMIT TRAN
RETURN 0
GO