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
	@FelhasznaloID int,
	@Jogkor nvarchar(255)
AS
	begin tran
		if not exists (select * from [dbo].[Jogkorok] 
			where FelhasznaloID = @FelhasznaloID)
		begin
		   insert into [dbo].[Jogkorok](FelhasznaloID, Jogkor) 
		   values (@FelhasznaloID,@Jogkor)
		end
	commit tran
RETURN 0
GO