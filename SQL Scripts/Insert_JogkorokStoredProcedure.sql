USE Nyilvantartas
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE SPECIFIC_SCHEMA = N'dbo'
		AND SPECIFIC_NAME = N'InsertJogkorok' 
)
   DROP PROCEDURE [dbo].[InsertJogkorok]
GO

CREATE PROCEDURE [dbo].[InsertJogkorok]
	@FelhasznaloID INT,
	@Jogkor NVARCHAR(255)
AS
	BEGIN TRAN
		IF NOT EXISTS (SELECT * FROM [dbo].[Jogkorok]
			WHERE FelhasznaloID = @FelhasznaloID)
		BEGIN
		   INSERT INTO [dbo].[Jogkorok](FelhasznaloID, Jogkor) 
		   VALUES (@FelhasznaloID,@Jogkor)
		END
	COMMIT TRAN
RETURN 0
GO