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
	@id int, 
	@Nev nvarchar, 
	@Jelszo nvarchar, 
	@Email nvarchar, 
	@Munkaido int
AS
	begin tran
		if exists (select * from [dbo].[Felhasznalok] 
			where Email=@Email)
		begin
		   update [dbo].[Felhasznalok] 
		   set 
				Nev=@Nev, 
				Email=@Email, 
				Munkaido=@Munkaido
		   where id=@id
		end
		else
		begin
		   insert into [dbo].[Felhasznalok](Nev, Jelszo, Email, Munkaido) 
		   values (@Nev, @Jelszo, @Email, @Munkaido)
		end
	commit tran
RETURN 0
GO