USE Nyilvantartas
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'UpdateJelszo' 
)
	DROP PROCEDURE [dbo].[UpdateJelszo]
GO

CREATE PROCEDURE [dbo].[UpdateJelszo]
	@Jelszo NVARCHAR(255), 
	@Email NVARCHAR(255)
AS
	BEGIN TRAN
		IF EXISTS (SELECT * FROM [dbo].[Felhasznalok] 
			WHERE Email = @Email)
		BEGIN
		   UPDATE [dbo].[Felhasznalok] 
		   SET 
				Email = @Email, 
				Jelszo = @Jelszo
		   WHERE Email = @Email
		END
	COMMIT TRAN
RETURN 0
GO