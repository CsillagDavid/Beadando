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
	@id int,
	@Nev nvarchar (255),
	@Email nvarchar (255)
AS
	begin tran
		if exists (select * from [dbo].[Felhasznalok] 
			where Email = @Email and id = @id)
		begin
		if exists (select * from [dbo].[Jogkorok]
			Where FelhasznaloID = @id)
			begin
				delete from [dbo].[Jogkorok]
				where FelhasznaloID = @id
			end
			if exists (select * from [dbo].[Munkaidok]
			Where FelhasznaloID = @id)
			begin
				delete from [dbo].[Munkaidok]
				where FelhasznaloID = @id
			end
			delete from [dbo].[Felhasznalok]
				where Email = @Email AND Nev = @Nev AND id = @id
		end	
	commit tran
RETURN 0
GO