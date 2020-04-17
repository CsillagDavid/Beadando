USE Nyilvantartas
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'UpdateJogkorok' 
)
   DROP PROCEDURE [dbo].[UpdateJogkorok]
GO

CREATE PROCEDURE [dbo].[UpdateJogkorok]
	@FelhasznaloID INT,
	@Jogkor NVARCHAR(255)
AS
	BEGIN TRAN
		IF EXISTS (SELECT * FROM [dbo].[Jogkorok] 
			WHERE FelhasznaloID = @FelhasznaloID)
		BEGIN
		   UPDATE [dbo].[Jogkorok] 
		   SET 
				Jogkor = @Jogkor
		   WHERE FelhasznaloID = @FelhasznaloID
		END
	COMMIT TRAN
RETURN 0
GO