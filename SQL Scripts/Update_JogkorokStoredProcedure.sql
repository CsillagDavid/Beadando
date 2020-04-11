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
	@FelhasznaloID int,
	@Jogkor nvarchar(255)
AS
	begin tran
		if exists (select * from [dbo].[Jogkorok] 
			where FelhasznaloID = @FelhasznaloID)
		begin
		   update [dbo].[Jogkorok] 
		   set 
				Jogkor = @Jogkor
		   where FelhasznaloID = @FelhasznaloID
		end
	commit tran
RETURN 0
GO